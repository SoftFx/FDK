// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LocalCSharp
{
	public static unsafe class Server
	{
		#region members
		private static readonly Channel m_channel = new Channel();
		private static readonly LocalServerInvokeHandler m_invoke = LrpInvoke;
		#endregion

		#region properties
		public static string LrpSignature
		{
			get
			{
				return Signature.Value;
			}
		}
		#endregion
		#region handlers of Simple component
		private static void LrpInvoke_Simple_Constructor(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.Simple;
			buffer.Reset(offset);
			var result = component.Constructor();
			buffer.WriteLocalPointer(result);
		}
		private static void LrpInvoke_Simple_Inverse(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.Simple;
			var arg0 = buffer.ReadAString();
			buffer.Reset(offset);
			var result = component.Inverse(arg0);
			buffer.WriteAString(result);
		}
		private static void LrpInvoke_Simple_Factorial(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.Simple;
			var arg0 = buffer.ReadInt32();
			var arg1 = default(int);
			buffer.Reset(offset);
			var result = component.Factorial(arg0, out arg1);
			buffer.WriteInt32(arg1);
			buffer.WriteBoolean(result);
		}

		private static readonly MethodHandler<Channel>[] gSimpleHandlers = new MethodHandler<Channel>[]
		{
			LrpInvoke_Simple_Constructor,
			LrpInvoke_Simple_Inverse,
			LrpInvoke_Simple_Factorial,
		};

		private static int LrpInvoke_Simple(int offset, int methodId, MemoryBuffer buffer, Channel channel)
		{
			if((methodId < 0) || (methodId >= 3))
			{
				return MagicNumbers.LRP_INVALID_METHOD_ID;
			}
			gSimpleHandlers[methodId](offset, buffer, channel);
			return MagicNumbers.S_OK;
		}

		#endregion

		#region handlers of Extended component
		private static void LrpInvoke_Extended_Do(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.Extended;
			var arg0 = buffer.ReadInType();
			var arg1 = buffer.ReadInOutType();
			var arg2 = default(LocalCSharp.OutType);
			buffer.Reset(offset);
			var result = component.Do(arg0, ref arg1, out arg2);
			buffer.WriteInOutType(arg1);
			buffer.WriteOutType(arg2);
			buffer.WriteReturnType(result);
		}
		private static void LrpInvoke_Extended_MarketBuy(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.Extended;
			var arg0 = buffer.ReadAString();
			var arg1 = buffer.ReadDouble();
			var arg2 = buffer.ReadDouble();
			var arg3 = default(double);
			buffer.Reset(offset);
			var result = component.MarketBuy(arg0, arg1, ref arg2, out arg3);
			buffer.WriteDouble(arg2);
			buffer.WriteDouble(arg3);
			buffer.WriteInt32(result);
		}

		private static readonly MethodHandler<Channel>[] gExtendedHandlers = new MethodHandler<Channel>[]
		{
			LrpInvoke_Extended_Do,
			LrpInvoke_Extended_MarketBuy,
		};

		private static int LrpInvoke_Extended(int offset, int methodId, MemoryBuffer buffer, Channel channel)
		{
			if((methodId < 0) || (methodId >= 2))
			{
				return MagicNumbers.LRP_INVALID_METHOD_ID;
			}
			gExtendedHandlers[methodId](offset, buffer, channel);
			return MagicNumbers.S_OK;
		}

		#endregion

		#region component handlers
		private static readonly ComponentHandler<Channel>[] gHandlers = new ComponentHandler<Channel>[]
		{
			LrpInvoke_Simple,
			LrpInvoke_Extended,
		};

		#endregion
		public static int LrpInitialize(string argument)
		{
			int result = LocalServer.Initialize(argument, Signature.Value, m_invoke);
			return result;
		}
		public static int LrpInvoke(ushort componentId, ushort methodId, void* heap, int* pSize, void** ppData, int* pCapacity)
		{
			try
			{
				MemoryBuffer buffer = new MemoryBuffer(heap, *ppData, *pSize, *pCapacity);
				int result = LrpInvokeEx(0, componentId, methodId, buffer, m_channel);
				*pSize = buffer.Size;
				*ppData = buffer.Data;
				*pCapacity = buffer.Capacity;
				buffer.Heap = null;
				return result;
			}
			catch(System.Exception)
			{
				return MagicNumbers.E_FAIL;
			}
		}
		private static int LrpInvokeEx(int offset, int componentId, int methodId, MemoryBuffer buffer, Channel channel)
		{
			if((componentId < 0) || (componentId >= 2))
			{
				return MagicNumbers.LRP_INVALID_COMPONENT_ID;
			}
			int result = MagicNumbers.LRP_EXCEPTION;
			try
			{
				try
				{
					result = gHandlers[componentId](offset, methodId, buffer, channel);
					return result;
				}
				catch(System.Exception e)
				{
					buffer.Reset(offset);
					buffer.WriteInt32(-1);
					buffer.WriteAString(e.Message);
				}
			}
			catch(System.Exception)
			{
				result = MagicNumbers.E_FAIL;
			}
			return result;
		}
	}
}
