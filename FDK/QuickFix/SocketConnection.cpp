/****************************************************************************
** Copyright (c) quickfixengine.org  All rights reserved.
**
** This file is part of the QuickFIX FIX Engine
**
** This file may be distributed under the terms of the quickfixengine.org
** license as defined by quickfixengine.org and appearing in the file
** LICENSE included in the packaging of this file.
**
** This file is provided AS IS with NO WARRANTY OF ANY KIND, INCLUDING THE
** WARRANTY OF DESIGN, MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
**
** See http://www.quickfixengine.org/LICENSE for licensing information.
**
** Contact ask@quickfixengine.org if any conditions of this licensing are
** not clear to you.
**
****************************************************************************/

#include "stdafx.h"

#include "SocketConnection.h"
#include "SocketConnector.h"
#include "SocketInitiator.h"
#include "Session.h"
#include "Utility.h"

namespace FIX
{
SocketConnection::SocketConnection( int s, Sessions sessions,
                                    SocketMonitor* pMonitor )
: m_socket( s ), m_sendLength( 0 ),
  m_sessions(sessions), m_pSession( 0 ), m_pMonitor( pMonitor )
{
  FD_ZERO( &m_fds );
  FD_SET( m_socket, &m_fds );
}

SocketConnection::SocketConnection( SocketInitiator& i,
                                    const SessionID& sessionID, int s,
                                    SocketMonitor* pMonitor )
: m_socket( s ), m_sendLength( 0 ),
  m_pSession( i.getSession( sessionID, *this ) ),
  m_pMonitor( pMonitor ) 
{
  FD_ZERO( &m_fds );
  FD_SET( m_socket, &m_fds );
  m_sessions.insert( sessionID );
}

SocketConnection::~SocketConnection()
{
  if ( m_pSession )
    Session::unregisterSession( m_pSession->getSessionID() );
}

bool SocketConnection::send( const std::string& msg )
{

  Locker l( m_mutex );

  m_sendQueue.push_back( msg );
  processQueue();
  signal();
  return true;

  
}

bool SocketConnection::processQueue()
{

  Locker l( m_mutex );

  if( !m_sendQueue.size() ) return true;

  struct timeval timeout = { 0, 0 };
  fd_set writeset = m_fds;
  if( select( 1 + m_socket, 0, &writeset, 0, &timeout ) <= 0 )
    return false;
    
  const std::string& msg = m_sendQueue.front();

  int result = socket_send
    ( m_socket, msg.c_str() + m_sendLength, static_cast<int>( msg.length() - m_sendLength ));

  if( result > 0 )
    m_sendLength += result;

  if( m_sendLength == msg.length() )
  {
    m_sendLength = 0;
    m_sendQueue.pop_front();
  }

  return !m_sendQueue.size();

  
}

void SocketConnection::disconnect()
{

  if ( m_pMonitor )
    m_pMonitor->drop( m_socket );

  
}

bool SocketConnection::read( SocketConnector& s )
{

  if ( !m_pSession ) return false;

  try
  {
    readFromSocket();
    readMessages( s.getMonitor() );
  }
  catch( SocketRecvFailed& e )
  {
    m_pSession->getLog()->onEvent( e.what() );
    return false;
  }
  return true;

  
}


bool SocketConnection::isValidSession()
{

  if( m_pSession == 0 )
    return false;
  SessionID sessionID = m_pSession->getSessionID();
  if( Session::isSessionRegistered(sessionID) )
    return false;
  return !( m_sessions.find(sessionID) == m_sessions.end() );

  
}

void SocketConnection::readFromSocket()
throw( SocketRecvFailed )
{

  int size = recv( m_socket, m_buffer, sizeof(m_buffer), 0 );
  if( size < 0 ) throw SocketRecvFailed( size );
  m_parser.addToStream( m_buffer, size );

  
}

bool SocketConnection::readMessage( std::string& msg )
{

  try
  {
    return m_parser.readFixMessage( msg );
  }
  catch ( MessageParseError& ) {}
  return true;

  
}

void SocketConnection::readMessages( SocketMonitor& s )
{
  if( !m_pSession ) return;

  std::string msg;
  while( readMessage( msg ) )
  {
    try
    {
      m_pSession->next( msg, UtcTimeStamp() );
    }
    catch ( InvalidMessage& )
    {
      if( !m_pSession->isLoggedOn() )
        s.drop( m_socket );
    }
  }
}

void SocketConnection::onTimeout()
{
  #ifdef _MSC_VER
  SetLastError(FX_CODE_ERROR_TIMEOUT);
  #endif
  if ( m_pSession ) m_pSession->next();
  
}

void SocketConnection::GetNetworkActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived)
{
	FxGetSocketActivity(m_socket, pLogicalBytesSent, pPhysicalBytesSent, pLogicalBytesReceived, pPhysicalBytesReceived);
}

} // namespace FIX
