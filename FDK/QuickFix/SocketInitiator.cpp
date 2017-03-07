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


#include "SocketInitiator.h"
#include "Session.h"
#include "Settings.h"

namespace FIX
{
SocketInitiator::SocketInitiator( Application& application,
                                  MessageStoreFactory& factory,
                                  const SessionSettings& settings, int mode )
throw( ConfigError )
: Initiator( application, factory, settings ),
  m_connector( 1 ), m_lastConnect( 0 ),
  m_reconnectInterval( 30 ), m_noDelay( false ), m_sendBufSize( 0 ),
  m_rcvBufSize( 0 ) , m_mode(mode)
{
}

SocketInitiator::SocketInitiator( Application& application,
                                  MessageStoreFactory& factory,
                                  const SessionSettings& settings,
                                  LogFactory& logFactory, int mode )
throw( ConfigError )
: Initiator( application, factory, settings, logFactory ),
  m_connector( 1 ), m_lastConnect( 0 ),
  m_reconnectInterval( 30 ), m_noDelay( false ), m_sendBufSize( 0 ),
  m_rcvBufSize( 0 ), m_mode(mode)
{
}

SocketInitiator::~SocketInitiator()
{
  SocketConnections::iterator i;
  for (i = m_connections.begin();
       i != m_connections.end(); ++i)
    delete i->second;

  for (i = m_pendingConnections.begin();
       i != m_pendingConnections.end(); ++i)
    delete i->second;
}

void SocketInitiator::onConfigure( const SessionSettings& s )
throw ( ConfigError )
{

  try { m_reconnectInterval = s.get().getLong( RECONNECT_INTERVAL ); }
  catch ( std::exception& ) {}
  if( s.get().has( SOCKET_NODELAY ) )
    m_noDelay = s.get().getBool( SOCKET_NODELAY );
  if( s.get().has( SOCKET_SEND_BUFFER_SIZE ) )
    m_sendBufSize = s.get().getLong( SOCKET_SEND_BUFFER_SIZE );
  if( s.get().has( SOCKET_RECEIVE_BUFFER_SIZE ) )
    m_rcvBufSize = s.get().getLong( SOCKET_RECEIVE_BUFFER_SIZE );

  
}

void SocketInitiator::onInitialize( const SessionSettings& s )
throw ( RuntimeError )
{
  
}

void SocketInitiator::onStart()
{

  connect();

  while ( !isStopped() )
    m_connector.block( *this );

  time_t start = 0;
  time_t now = 0;

  ::time( &start );
  while ( isLoggedOn() )
  {
    m_connector.block( *this );
    if( ::time(&now) -5 >= start )
      break;
  }

  
}

bool SocketInitiator::onPoll( double timeout )
{

  time_t start = 0;
  time_t now = 0;

  if( isStopped() )
  {
    if( start == 0 )
      ::time( &start );
    if( !isLoggedOn() )
      return false;
    if( ::time(&now) - 5 >= start )
      return false;
  }

  m_connector.block( *this, true, timeout );
  return true;

  
}

void SocketInitiator::onStop()
{
  
}

void SocketInitiator::doConnect( const SessionID& s, const Dictionary& d )
{

  try
  {
    Session* session = Session::lookupSession( s );
    if( !session->isSessionTime(UtcTimeStamp()) ) return;

    Log* log = session->getLog();

    std::string address;
    short port = 0;
    std::string proxyType;
    std::string proxyAddress;
    short proxyPort;
    std::string proxyUserName;
    std::string proxyPassword;
    getHost( s, d, address, port, proxyType, proxyAddress, proxyPort, proxyUserName, proxyPassword);

    ConnectType connectType;

    if (proxyType == "")
    {
        connectType = ConnectType_Direct;
        log->onEvent("Connecting to " + address + " on port " + IntConvertor::convert((unsigned short)port));
    }
    else if (proxyType == "Socks4")
    {
        connectType = ConnectType_ProxySocks4;
        log->onEvent("Connecting to " + address + " on port " + IntConvertor::convert((unsigned short)port) + " via socks 4 proxy " + proxyAddress + " on port " + IntConvertor::convert((unsigned short)proxyPort) + " as " + proxyUserName);
    }
    else if (proxyType == "Socks5")
    {
        connectType = ConnectType_ProxySocks5;
        log->onEvent("Connecting to " + address + " on port " + IntConvertor::convert((unsigned short)port) + " via socks 5 proxy " + proxyAddress + " on port " + IntConvertor::convert((unsigned short)proxyPort) + " as " + proxyUserName);
    }
    else
        throw ConfigError("Invalid proxy type : " + proxyType);
    
    int result = m_connector.connect(connectType, address, port, m_noDelay, m_sendBufSize, m_rcvBufSize, m_mode, proxyAddress, proxyPort, proxyUserName, proxyPassword );

    if( result != -1 )
    {
      setPending( s );

      m_pendingConnections[ result ] 
        = new SocketConnection( *this, s, result, &m_connector.getMonitor() );
    }
	else if(nullptr != session)
	{
		session->disconnect();
	}
  }
  catch ( std::exception& ) {}

  
}

void SocketInitiator::onConnect( SocketConnector&, int s )
{

  SocketConnections::iterator i = m_pendingConnections.find( s );
  if( i == m_pendingConnections.end() ) return;
  SocketConnection* intConnection = i->second;
  
  m_connections[s] = intConnection;
  m_pendingConnections.erase( i );
  setConnected( intConnection->getSession()->getSessionID() );
  intConnection->onTimeout();

  
}

void SocketInitiator::onWrite( SocketConnector& connector, int s )
{

  SocketConnections::iterator i = m_connections.find( s );
  if ( i == m_connections.end() ) return ;
  SocketConnection* intConnection = i->second;
  if( intConnection->processQueue() )
    intConnection->unsignal();
  
  
}

bool SocketInitiator::onData( SocketConnector& connector, int s )
{

  SocketConnections::iterator i = m_connections.find( s );
  if ( i == m_connections.end() ) return false;
  SocketConnection* intConnection = i->second;
  return intConnection->read( connector );

  
}

void SocketInitiator::onDisconnect( SocketConnector&, int s )
{

  SocketConnections::iterator i = m_connections.find( s );
  SocketConnections::iterator j = m_pendingConnections.find( s );

  SocketConnection* intConnection = 0;
  if( i != m_connections.end() ) 
	  intConnection = i->second;
  if( j != m_pendingConnections.end() )
	  intConnection = j->second;
  if( !intConnection )
	  return;

  setDisconnected( intConnection->getSession()->getSessionID() );

  Session* pSession = intConnection->getSession();
  if ( pSession )
  {
    pSession->disconnect();
    setDisconnected( pSession->getSessionID() );
  }

  delete intConnection;
  m_connections.erase( s );
  m_pendingConnections.erase( s );

  
}

void SocketInitiator::onError( SocketConnector& connector )
{
  onTimeout( connector );
  
}

void SocketInitiator::onTimeout( SocketConnector& )
{

  time_t now;
  ::time( &now );

  if ( (now - m_lastConnect) >= m_reconnectInterval )
  {
    connect();
    m_lastConnect = now;
  }

  SocketConnections::iterator i;
  for ( i = m_connections.begin(); i != m_connections.end(); ++i )
    i->second->onTimeout();

  
}

void SocketInitiator::getHost( const SessionID& s, const Dictionary& d,
    std::string& address, short& port, std::string& porxyType, 
    std::string& proxyAddress, short& proxyPort,
    std::string& proxyUserName, std::string& proxyPassword)
{

  int num = 0;
  SessionToHostNum::iterator i = m_sessionToHostNum.find( s );
  if ( i != m_sessionToHostNum.end() ) num = i->second;

  std::stringstream hostStream;
  hostStream << SOCKET_CONNECT_HOST << num;
  std::string hostString = hostStream.str();

  std::stringstream portStream;  
  portStream << SOCKET_CONNECT_PORT << num;
  std::string portString = portStream.str();

  if( d.has(hostString) && d.has(portString) )
  {
    address = d.getString( hostString );
    port = ( short ) d.getLong( portString );
  }
  else
  {
    num = 0;
    address = d.getString( SOCKET_CONNECT_HOST );
    port = ( short ) d.getLong( SOCKET_CONNECT_PORT );
  }

  std::stringstream proxyTypeSream;
  proxyTypeSream << PROXY_TYPE << num;
  std::string proxyTypeString = proxyTypeSream.str();

  if (d.has(proxyTypeString))
  {
      porxyType = d.getString(proxyTypeString);
  }
  else if (d.has(PROXY_TYPE))
  {
      porxyType = d.getString(PROXY_TYPE);
  }
  else
      porxyType = "";

  std::stringstream proxyAddressStream;
  proxyAddressStream << PROXY_ADDRESS << num;
  std::string proxyAddressString = proxyAddressStream.str();

  std::stringstream proxyPortStream;
  proxyPortStream << PROXY_PORT << num;
  std::string proxyPortString = proxyPortStream.str();  

  if (d.has(proxyAddressString) && d.has(proxyPortString))
  {
      proxyAddress = d.getString(proxyAddressString);
      proxyPort = (short)d.getLong(proxyPortString);
  }
  else if (d.has(PROXY_ADDRESS) && d.has(PROXY_PORT))
  {
      proxyAddress = d.getString(PROXY_ADDRESS);
      proxyPort = (short)d.getLong(PROXY_PORT);
  }
  else
  {
      proxyAddress = "";
      proxyPort = 0;
  }

  std::stringstream proxyUserNameSream;
  proxyUserNameSream << PROXY_USERNAME << num;
  std::string proxyUserNameString = proxyUserNameSream.str();

  if (d.has(proxyUserNameString))
  {
      proxyUserName = d.getString(proxyUserNameString);
  }
  else if (d.has(PROXY_USERNAME))
  {
      proxyUserName = d.getString(PROXY_USERNAME);
  }
  else
      proxyUserName = "";

  std::stringstream proxyPasswordSream;
  proxyPasswordSream << PROXY_PASSWORD << num;
  std::string proxyPasswordString = proxyPasswordSream.str();

  if (d.has(proxyPasswordString))
  {
      proxyPassword = d.getString(proxyPasswordString);
  }
  else if (d.has(PROXY_PASSWORD))
  {
      proxyPassword = d.getString(PROXY_PASSWORD);
  }
  else
      proxyPassword = "";

  m_sessionToHostNum[ s ] = ++num;  
}

void SocketInitiator::GetNetworkActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived)
{
	if (1 != m_connections.size())
	{
		return;
	}
	SocketConnection* pConnection = m_connections.begin()->second;
	pConnection->GetNetworkActivity(pLogicalBytesSent, pPhysicalBytesSent, pLogicalBytesReceived, pPhysicalBytesReceived);
}

}
