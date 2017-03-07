/* -*- C++ -*- */

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

#ifndef FIX_SOCKETINITIATOR_H
#define FIX_SOCKETINITIATOR_H

#ifdef _MSC_VER
#pragma warning( disable : 4503 4355 4786 4290 )
#endif

#include "Initiator.h"
#include "SocketConnector.h"
#include "SocketConnection.h"

namespace FIX
{
/// Socket implementation of Initiator.
class SocketInitiator : public Initiator, SocketConnector::Strategy
{
public:
  SocketInitiator( Application& application, MessageStoreFactory& factory,
                   const SessionSettings& settings, int mode) throw( ConfigError );
  SocketInitiator( Application& application, MessageStoreFactory& factory,
                   const SessionSettings& settings, LogFactory& logFactory, int mode) throw( ConfigError );

  virtual ~SocketInitiator();
public:
	void GetNetworkActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived);
private:
  typedef std::map < int, SocketConnection* > SocketConnections;
  typedef std::map < SessionID, int > SessionToHostNum;

  void onConfigure( const SessionSettings& ) throw ( ConfigError );
  void onInitialize( const SessionSettings& ) throw ( RuntimeError );

  void onStart();
  bool onPoll( double timeout );
  void onStop();

  void doConnect( const SessionID&, const Dictionary& d );
  void onConnect( SocketConnector&, int );
  void onWrite( SocketConnector&, int );
  bool onData( SocketConnector&, int );
  void onDisconnect( SocketConnector&, int );
  void onError( SocketConnector& );
  void onTimeout( SocketConnector& );

  void getHost( const SessionID&, const Dictionary&, std::string&, short&, 
      std::string& proxyType, std::string& proxyAddress, short& proxyPort, 
      std::string& proxyUserName, std::string& proxyPassword);

  SessionSettings m_settings;
  SessionToHostNum m_sessionToHostNum;
  SocketConnector m_connector;
  SocketConnections m_pendingConnections;
  SocketConnections m_connections;
  time_t m_lastConnect;
  int m_reconnectInterval;
  bool m_noDelay;
  int m_sendBufSize;
  int m_rcvBufSize;
  bool m_stop;
  int m_mode;
};
/*! @} */
}

#endif //FIX_SOCKETINITIATOR_H
