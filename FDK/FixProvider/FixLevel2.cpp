#include "stdafx.h"
#include "FixLevel2.h"
#include "../quickfix/FieldConvertors.h"

namespace
{
	const char SOH = '\001';
	const string c35_34 = string("35=W\1") + string("34=");
	const string c49 = SOH + string("49=");
	const string c52 = SOH + string("52=");
	const string c56 = SOH + string("56=");
	const string c55 = SOH + string("55=");
	const string c268 = SOH + string("268=");
	const string c269_0_270 = string("269=0\1") + string("270=");
	const string c269_1_270 = string("269=1\1") + string("270=");
	const string c271 = SOH + string("271=");
	const string c8_9 = string("8=FIX.4.4\1") + string("9=");
	const string c10 = "10=";
}

namespace
{
	void FormatFixBodyOfLevel2(const ptrdiff_t sequenceNumber, const CFxQuote& quote, CMemoryStream& stream)
	{
		char buffer[32];

		const string& symbol = quote.Symbol;
		const FIX::UtcTimeStamp settingTime = quote.CreatingTime;

		stream << c35_34;
		stream << _itoa_s(static_cast<int>(sequenceNumber), buffer, 32, 10);
		stream << c49 << "{BB5FF43C-80D6-44AD-96D4-7D2F54AAA185}";
		stream << c52 << FIX::UtcTimeStampConvertor::convert(settingTime, true);
		stream << c56 << "EXECUTOR";
		stream << c55 << symbol;

		const vector<CFxQuoteEntry>& bids = quote.Bids;
		const vector<CFxQuoteEntry>& asks = quote.Asks;

		stream << c268 << _itoa_s(static_cast<int>(bids.size() + asks.size()), buffer, 32, 10) << SOH;


		for each(const auto& element in bids)
		{
			stream << c269_0_270;

			double price = element.Price;
			sprintf_s(buffer, 32, "%.15g", price);
			stream << buffer;

			double volume = element.Volume;
			int _volume = static_cast<int>(volume);
			if (_volume == volume)
			{
				_itoa_s(_volume, buffer, 32, 10);
			}
			else
			{
				sprintf_s(buffer, 32, "%.15g", volume);
			}
			stream << c271 << buffer << SOH;
		}
		for each(const auto& element in asks)
		{
			stream << c269_1_270;

			double price = element.Price;
			sprintf_s(buffer, 32, "%.15g", price);
			stream << buffer;

			double volume = element.Volume;
			int _volume = static_cast<int>(volume);
			if (_volume == volume)
			{
				_itoa_s(_volume, buffer, 32, 10);
			}
			else
			{
				sprintf_s(buffer, 32, "%.15g", volume);
			}
			stream << c271 << buffer << SOH;
		}
	}
}


int CalcFastFixCodec(const ptrdiff_t sequenceNumber, const CFxQuote& quote)
{
	char buffer[32];
	string _st;
	CMemoryStream _stream;
	FormatFixBodyOfLevel2(sequenceNumber, quote, _stream);
	string body = _stream.str();

	CMemoryStream stream2;
	stream2 << c8_9;
	stream2 << _itoa_s(static_cast<int>(body.length()), buffer, 32, 10) << SOH;
	string header = stream2.str();

	int crc = std::accumulate(header.begin(), header.end(), 0);
	crc = std::accumulate(body.begin(), body.end(), crc);
	crc = (crc % 256);

	stringstream stream;

	stream << header;
	stream << body;
	stream << c10 << FIX::CheckSumConvertor::convert(crc) << SOH;

	string st = stream.str();

	int result = static_cast<int>(st.length());
	return result;
}

int CalcSlowFixCodec(const ptrdiff_t sequenceNumber, const CFxQuote& quote)
{

	FIX44::MarketDataSnapshotFullRefresh msg;
	msg.SetSymbol(quote.Symbol);
	msg.getHeader().SetSendingTime(quote.CreatingTime);
	msg.getHeader().SetSenderCompID("{BB5FF43C-80D6-44AD-96D4-7D2F54AAA185}");
	msg.getHeader().SetTargetCompID("EXECUTOR");
	FIX::MsgSeqNum seqNum;
	seqNum.setValue(static_cast<int>(sequenceNumber));

	msg.getHeader().setField(seqNum);

	for each(const auto& element in quote.Bids)
	{
		FIX44::MarketDataSnapshotFullRefresh::NoMDEntries group;
		group.SetMDEntryType(FIX::MDEntryType_BID);
		group.SetMDEntryPx(element.Price);
		group.SetMDEntrySize(element.Volume);
		msg.addGroup(group);
	}

	for each(const auto& element in quote.Asks)
	{
		FIX44::MarketDataSnapshotFullRefresh::NoMDEntries group;
		group.SetMDEntryType(FIX::MDEntryType_OFFER);
		group.SetMDEntryPx(element.Price);
		group.SetMDEntrySize(element.Volume);
		msg.addGroup(group);
	}

	string st = msg.toString();
	const int result = static_cast<int>(st.size());

	return result;
}
