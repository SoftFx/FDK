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


#include "FieldTypes.h"

#ifdef HAVE_FTIME
# include <sys/timeb.h
#endif

namespace FIX {

DateTime DateTime::nowUtc()
{
  timeb tb;
  ftime (&tb);
   return fromUtcTimeT (tb.time, tb.millitm);
}

DateTime DateTime::nowLocal()
{
#if defined( HAVE_FTIME )
    timeb tb;
    ftime (&tb);
    return fromLocalTimeT( tb.time, tb.millitm );
#elif defined( _POSIX_SOURCE )
    struct timeval tv;
    gettimeofday (&tv, 0);
    return fromLocalTimeT( tv.tv_sec, tv.tv_usec / 1000 );
#else
    return fromLocalTimeT( ::time (0), 0 );
#endif
}

}
