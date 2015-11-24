// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LrpServer.Net.LocalCSharp
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
		#region handlers of LocalServerHandler component
		private static void LrpInvoke_LocalServerHandler_BeginNewConnectionRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadInt32();
			buffer.Reset(offset);
			component.BeginNewConnectionRequest(arg0, arg1, arg2, arg3);
		}
		private static void LrpInvoke_LocalServerHandler_BeginShutdownConnectionNotification(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			buffer.Reset(offset);
			component.BeginShutdownConnectionNotification(arg0, arg1);
		}
		private static void LrpInvoke_LocalServerHandler_BeginLogonRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadInt32();
			var arg4 = buffer.ReadAString();
			var arg5 = buffer.ReadAString();
			buffer.Reset(offset);
			component.BeginLogonRequest(arg0, arg1, arg2, arg3, arg4, arg5);
		}
		private static void LrpInvoke_LocalServerHandler_BeginLogoutRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			buffer.Reset(offset);
			component.BeginLogoutRequest(arg0, arg1);
		}
		private static void LrpInvoke_LocalServerHandler_BeginCurrenciesInfoRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			buffer.Reset(offset);
			component.BeginCurrenciesInfoRequest(arg0, arg1, arg2);
		}
		private static void LrpInvoke_LocalServerHandler_BeginSymbolsInfoRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			buffer.Reset(offset);
			component.BeginSymbolsInfoRequest(arg0, arg1, arg2);
		}
		private static void LrpInvoke_LocalServerHandler_BeginSessionInfoRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			buffer.Reset(offset);
			component.BeginSessionInfoRequest(arg0, arg1, arg2);
		}
		private static void LrpInvoke_LocalServerHandler_BeginSubscribeToQuotesRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadAStringArray();
			var arg4 = buffer.ReadInt32();
			buffer.Reset(offset);
			component.BeginSubscribeToQuotesRequest(arg0, arg1, arg2, arg3, arg4);
		}
		private static void LrpInvoke_LocalServerHandler_BeginUnsubscribeQuotesRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadAStringArray();
			buffer.Reset(offset);
			component.BeginUnsubscribeQuotesRequest(arg0, arg1, arg2, arg3);
		}
		private static void LrpInvoke_LocalServerHandler_BeginComponentsInfoRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadInt32();
			buffer.Reset(offset);
			component.BeginComponentsInfoRequest(arg0, arg1, arg2, arg3);
		}
		private static void LrpInvoke_LocalServerHandler_BeginDataHistoryRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadDataHistoryRequest();
			buffer.Reset(offset);
			component.BeginDataHistoryRequest(arg0, arg1, arg2, arg3);
		}
		private static void LrpInvoke_LocalServerHandler_BeginFileChunkRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadAString();
			var arg4 = buffer.ReadUInt32();
			buffer.Reset(offset);
			component.BeginFileChunkRequest(arg0, arg1, arg2, arg3, arg4);
		}
		private static void LrpInvoke_LocalServerHandler_BeginBarsHistoryMetaInfoFileRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadAString();
			var arg4 = buffer.ReadInt32();
			var arg5 = buffer.ReadAString();
			buffer.Reset(offset);
			component.BeginBarsHistoryMetaInfoFileRequest(arg0, arg1, arg2, arg3, arg4, arg5);
		}
		private static void LrpInvoke_LocalServerHandler_BeginQuotesHistoryMetaInfoFileRequest(int offset, MemoryBuffer buffer, Channel channel)
		{
			var component = channel.LocalServerHandler;
			var arg0 = buffer.ReadLocalPointer();
			var arg1 = buffer.ReadInt64();
			var arg2 = buffer.ReadAString();
			var arg3 = buffer.ReadAString();
			var arg4 = buffer.ReadBoolean();
			buffer.Reset(offset);
			component.BeginQuotesHistoryMetaInfoFileRequest(arg0, arg1, arg2, arg3, arg4);
		}

		private static readonly MethodHandler<Channel>[] gLocalServerHandlerHandlers = new MethodHandler<Channel>[]
		{
			LrpInvoke_LocalServerHandler_BeginNewConnectionRequest,
			LrpInvoke_LocalServerHandler_BeginShutdownConnectionNotification,
			LrpInvoke_LocalServerHandler_BeginLogonRequest,
			LrpInvoke_LocalServerHandler_BeginLogoutRequest,
			LrpInvoke_LocalServerHandler_BeginCurrenciesInfoRequest,
			LrpInvoke_LocalServerHandler_BeginSymbolsInfoRequest,
			LrpInvoke_LocalServerHandler_BeginSessionInfoRequest,
			LrpInvoke_LocalServerHandler_BeginSubscribeToQuotesRequest,
			LrpInvoke_LocalServerHandler_BeginUnsubscribeQuotesRequest,
			LrpInvoke_LocalServerHandler_BeginComponentsInfoRequest,
			LrpInvoke_LocalServerHandler_BeginDataHistoryRequest,
			LrpInvoke_LocalServerHandler_BeginFileChunkRequest,
			LrpInvoke_LocalServerHandler_BeginBarsHistoryMetaInfoFileRequest,
			LrpInvoke_LocalServerHandler_BeginQuotesHistoryMetaInfoFileRequest,
		};

		private static int LrpInvoke_LocalServerHandler(int offset, int methodId, MemoryBuffer buffer, Channel channel)
		{
			if((methodId < 0) || (methodId >= 14))
			{
				return MagicNumbers.LRP_INVALID_METHOD_ID;
			}
			gLocalServerHandlerHandlers[methodId](offset, buffer, channel);
			return MagicNumbers.S_OK;
		}

		#endregion

		#region component handlers
		private static readonly ComponentHandler<Channel>[] gHandlers = new ComponentHandler<Channel>[]
		{
			LrpInvoke_LocalServerHandler,
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
			if((componentId < 0) || (componentId >= 1))
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
