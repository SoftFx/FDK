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

#ifndef FIX_NULLSTORE_H
#define FIX_NULLSTORE_H

#ifdef _MSC_VER
#pragma warning( disable : 4503 4355 4786 4290 )
#endif

#include "MessageStore.h"
#include "SessionSettings.h"
#include <string>

namespace FIX
{
class Session;

/**
 * Null implementation of MessageStore.
 *
 * Will not actually store messages.  Useful for admin-only or market
 * data-only applications.
 */
class NullStoreFactory : public MessageStoreFactory
{
public:
  MessageStore* create( const SessionID& );
  void destroy( MessageStore* );
};
/*! @} */


/**
 * Null implementation of MessageStore.
 *
 * Will not actually store messages.  Useful for admin-only or market
 * data-only applications.
 */
class NullStore : public MessageStore
{
public:
  NullStore() : m_nextSenderMsgSeqNum( 1 ), m_nextTargetMsgSeqNum( 1 ) {}

  bool set( int, const std::string& ) throw ( IOException );
  void get( int, int, std::vector < std::string > & ) const throw ( IOException );

  int getNextSenderMsgSeqNum() const throw ( IOException )
  { return m_nextSenderMsgSeqNum; }
  int getNextTargetMsgSeqNum() const throw ( IOException )
  { return m_nextTargetMsgSeqNum; }
  void setNextSenderMsgSeqNum( int value ) throw ( IOException )
  { m_nextSenderMsgSeqNum = value; }
  void setNextTargetMsgSeqNum( int value ) throw ( IOException )
  { m_nextTargetMsgSeqNum = value; }
  void incrNextSenderMsgSeqNum() throw ( IOException )
  { ++m_nextSenderMsgSeqNum; }
  void incrNextTargetMsgSeqNum() throw ( IOException )
  { ++m_nextTargetMsgSeqNum; }
  void decrNextSenderMsgSeqNum() throw ( IOException )
  { --m_nextSenderMsgSeqNum; }
  void decrNextTargetMsgSeqNum() throw ( IOException )
  { --m_nextTargetMsgSeqNum; }

  void setCreationTime( const UtcTimeStamp& creationTime ) throw ( IOException )
  { m_creationTime = creationTime; }
  UtcTimeStamp getCreationTime() const throw ( IOException )
  { return m_creationTime; }

  void reset() throw ( IOException )
  {
    m_nextSenderMsgSeqNum = 1; m_nextTargetMsgSeqNum = 1;
    m_creationTime.setCurrent();
  }
  void refresh() throw ( IOException ) {}

private:
  int m_nextSenderMsgSeqNum;
  int m_nextTargetMsgSeqNum;
  UtcTimeStamp m_creationTime;
};
}

#endif // FIX_NULLSTORE_H

