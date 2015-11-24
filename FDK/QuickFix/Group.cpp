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


#include "Group.h"

namespace FIX
{
void Group::addGroup( Group& group )
{
  FieldMap::addGroup( group.field(), group );
  
}

void Group::replaceGroup( unsigned num, FIX::Group& group )
{ 
  FieldMap::replaceGroup( num, group.field(), group ); 
  
}

Group& Group::getGroup( unsigned num, Group& group ) const throw( FieldNotFound )
{
  return static_cast < Group& > ( FieldMap::getGroup( num, group.field(), group ) );
  
}

void Group::removeGroup( unsigned num, Group& group )
{
  FieldMap::removeGroup( num, group.field() );
  
}

void Group::removeGroup( Group& group )
{
  FieldMap::removeGroup( group.field() );
  
}

bool Group::hasGroup( unsigned num, Group& group )
{
  return FieldMap::hasGroup( num, group.field() );
  
}

bool Group::hasGroup( const Group& group )
{
  return FieldMap::hasGroup( group.field() );
  
}
}
