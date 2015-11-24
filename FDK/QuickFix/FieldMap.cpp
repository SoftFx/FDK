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


#include "FieldMap.h"



namespace FIX
{
FieldMap::~FieldMap()
{
  clear();
  
}

FieldMap& FieldMap::operator=( const FieldMap& rhs )
{

  m_fields = rhs.m_fields;

  clear();

  std::copy( rhs.m_fields.begin (), rhs.m_fields.end(),
             std::inserter(m_fields, m_fields.begin()) );

  Groups::const_iterator i;
  for ( i = rhs.m_groups.begin(); i != rhs.m_groups.end(); ++i )
  {
    std::vector < FieldMap* > ::const_iterator j;
    for ( j = i->second.begin(); j != i->second.end(); ++j )
      addGroup( i->first, **j );
  }

  return *this;

  
}

void FieldMap::addGroup( int field, const FieldMap& group, bool setCount )
{

  FieldMap * pGroup = new FieldMap( group.m_fields.key_comp() );
  *pGroup = group;
  m_groups[ field ].push_back( pGroup );
  Groups::iterator i = m_groups.find( field );
  if( setCount )
    setField( IntField( field, static_cast<int>(i->second.size() )) );

  
}

void FieldMap::replaceGroup( int num, int field, FieldMap& group )
{

  Groups::const_iterator i = m_groups.find( field );
  if ( i == m_groups.end() ) return;
  if ( num <= 0 ) return;
  if ( i->second.size() < ( unsigned ) num ) return;
  *( *( i->second.begin() + ( num - 1 ) ) ) = group;

  
}

void FieldMap::removeGroup( int num, int field )
{
  Groups::iterator i = m_groups.find( field );
  if ( i == m_groups.end() ) return;
  if ( num <= 0 ) return;
  std::vector< FieldMap* >& vector = i->second;
  if ( vector.size() < ( unsigned ) num ) return;

  std::deque< FieldMap* > queue;
  while( vector.size() > (unsigned)num )
  {
    queue.push_back( vector.back() );
    vector.pop_back();
  }
  delete vector.back();
  vector.pop_back();
  while( queue.size() )
  {
    vector.push_back( queue.front() );
    queue.pop_front();
  }

  if( vector.size() == 0 )
  {
    m_groups.erase( field );
  }
  else
  {
    IntField groupCount( field, static_cast<int>(vector.size()) );
    setField( groupCount, true );
  }
}

void FieldMap::removeGroup( int field )
{
  removeGroup( groupCount(field), field );
  
}

void FieldMap::removeField( int field )
{

  Fields::iterator i = m_fields.find( field );
  if ( i != m_fields.end() )
    m_fields.erase( i );

  
}

bool FieldMap::hasGroup( int num, int field ) const
{

  return groupCount(field) >= num;

  
}

bool FieldMap::hasGroup( int field ) const
{

  Groups::const_iterator i = m_groups.find( field );
  return i != m_groups.end();

  
}

int FieldMap::groupCount( int field ) const
{

  Groups::const_iterator i = m_groups.find( field );
  if( i == m_groups.end() )
    return 0;
  return static_cast<int>(i->second.size());

  
}

void FieldMap::clear()
{

  m_fields.clear();

  Groups::iterator i;
  for ( i = m_groups.begin(); i != m_groups.end(); ++i )
  {
    std::vector < FieldMap* > ::iterator j;
    for ( j = i->second.begin(); j != i->second.end(); ++j )
      delete *j;
  }
  m_groups.clear();

  
}

bool FieldMap::isEmpty()
{
  return m_fields.size() == 0;
  
}

int FieldMap::totalFields() const
{
  int result = static_cast<int>(m_fields.size());
    
  Groups::const_iterator i;
  for ( i = m_groups.begin(); i != m_groups.end(); ++i )
  {
    std::vector < FieldMap* > ::const_iterator j;
    for ( j = i->second.begin(); j != i->second.end(); ++j )
      result += ( *j ) ->totalFields();
  }
  return result;
}

std::string& FieldMap::calculateString( std::string& result, bool clear ) const
{

#if defined(_MSC_VER) && _MSC_VER < 1300
  if( clear ) result = "";
#else
  if( clear ) result.clear();
#endif

  if( !result.size() )
    result.reserve( totalFields() * 32 );
    
  Fields::const_iterator i;
  for ( i = m_fields.begin(); i != m_fields.end(); ++i )
  {
    result += i->second.getValue();

    // add groups if they exist
    if( !m_groups.size() ) continue;
    Groups::const_iterator j = m_groups.find( i->first );
    if ( j == m_groups.end() ) continue;
    std::vector < FieldMap* > ::const_iterator k;
    for ( k = j->second.begin(); k != j->second.end(); ++k )
      ( *k ) ->calculateString( result, false );
  }
  return result;

  
}

int FieldMap::calculateLength( int beginStringField,
                               int bodyLengthField,
                               int checkSumField ) const
{
  int result = 0;
  Fields::const_iterator i;
  for ( i = m_fields.begin(); i != m_fields.end(); ++i )
  {
    if ( i->first != beginStringField
         && i->first != bodyLengthField
         && i->first != checkSumField )
    { result += i->second.getLength(); }
  }

  Groups::const_iterator j;
  for ( j = m_groups.begin(); j != m_groups.end(); ++j )
  {
    std::vector < FieldMap* > ::const_iterator k;
    for ( k = j->second.begin(); k != j->second.end(); ++k )
      result += ( *k ) ->calculateLength();
  }
  return result;
}

int FieldMap::calculateTotal( int checkSumField ) const
{

  int result = 0;
  Fields::const_iterator i;
  for ( i = m_fields.begin(); i != m_fields.end(); ++i )
  {
    if ( i->first != checkSumField )
      result += i->second.getTotal();
  }

  Groups::const_iterator j;
  for ( j = m_groups.begin(); j != m_groups.end(); ++j )
  {
    std::vector < FieldMap* > ::const_iterator k;
    for ( k = j->second.begin(); k != j->second.end(); ++k )
      result += ( *k ) ->calculateTotal();
  }
  return result;

  
}
}
