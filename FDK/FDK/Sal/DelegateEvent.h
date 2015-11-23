#ifndef __Sal_DelegateEvent__
#define __Sal_DelegateEvent__


#ifdef new
#pragma push_macro("new")
#undef new
#define DFTS_RESTORE_NEW
#endif


template<typename T, typename Signature> class Event;




#pragma region classes generation

// R ()
#define FX_TYPES
#define FX_PARAMS
#define FX_ARGS

#include "DelegateEvent.hpp"

#undef FX_TYPES
#undef FX_PARAMS
#undef FX_ARGS

// R (P0 a0)

#define FX_TYPES , typename P0
#define FX_PARAMS P0 a0
#define FX_ARGS a0

#include "DelegateEvent.hpp"

#undef FX_TYPES
#undef FX_PARAMS
#undef FX_ARGS

// R (P0 a0, P1 a1)

#define FX_TYPES , typename P0, typename P1
#define FX_PARAMS P0 a0, P1 a1
#define FX_ARGS a0, a1

#include "DelegateEvent.hpp"

#undef FX_TYPES
#undef FX_PARAMS
#undef FX_ARGS



// R (P0 a0, P1 a1, P2 a2)

#define FX_TYPES , typename P0, typename P1, typename P2
#define FX_PARAMS P0 a0, P1 a1, P2 a2
#define FX_ARGS a0, a1, a2

#include "DelegateEvent.hpp"

#undef FX_TYPES
#undef FX_PARAMS
#undef FX_ARGS


// R (P0 a0, P1 a1, P2 a2, P3 a3)

#define FX_TYPES , typename P0, typename P1, typename P2, typename P3
#define FX_PARAMS P0 a0, P1 a1, P2 a2, P3 a3
#define FX_ARGS a0, a1, a2, a3

#include "DelegateEvent.hpp"

#undef FX_TYPES
#undef FX_PARAMS
#undef FX_ARGS

#pragma endregion



#ifdef DFTS_RESTORE_NEW
#pragma pop_macro("new")
#undef DFTS_RESTORE_NEW
#endif
#endif
