#include "stdafx.h"
#include "TradeEntry.h"
#include "Functions.h"
#include "Rates.h"
#include "SymbolEntry.h"
#include "Formating.h"

namespace FDK
{

    CTradeEntry::CTradeEntry()
    {
        m_type = TradeType_None;
        m_side = TradeSide_None;
        m_volume = 0;
        m_price = nullptr;
        m_stopPrice = nullptr;
        m_commission = 0;
        m_agentCommission = 0;
        m_swap = 0;
        m_profitStatus = TradeEntryStatus_NotCalculated;
        m_marginStatus = TradeEntryStatus_NotCalculated;
    }

    CTradeEntry::CTradeEntry(const CTradeEntry& entry)
        : Tag(entry.Tag)
        , m_type(entry.m_type)
        , m_side(entry.m_side)
        , m_symbol(entry.m_symbol)
        , m_volume(entry.m_volume)
        , m_price(entry.m_price)
        , m_stopPrice(entry.m_stopPrice)
        , m_commission(entry.m_commission)
        , m_agentCommission(entry.m_agentCommission)
        , m_swap(entry.m_swap)
        , m_staticMarginRate(entry.m_staticMarginRate)
        , m_profitStatus(entry.m_profitStatus)
        , m_marginStatus(entry.m_marginStatus)
        , m_profit(entry.m_profit)
        , m_margin(entry.m_margin)
    {
    }

    CTradeEntry& CTradeEntry::operator= (const CTradeEntry& entry)
    {
        if (this != &entry)
        {
            Tag = entry.Tag;
            m_type = entry.m_type;
            m_side = entry.m_side;
            m_symbol = entry.m_symbol;
            m_volume = entry.m_volume;
            m_price = entry.m_price;
            m_stopPrice = entry.m_stopPrice;
            m_staticMarginRate = entry.m_staticMarginRate;
            m_profitStatus = entry.m_profitStatus;
            m_marginStatus = entry.m_marginStatus;
            m_profit = entry.m_profit;
            m_margin = entry.m_margin;
        }
        return *this;
    }

    CTradeEntry::~CTradeEntry()
    {
    }

#ifdef new
    #pragma push_macro("new")
    #undef new
    #define FX_RESTORE_NEW
#endif

    void CTradeEntry::Construct(TradeType type, TradeSide side, const char* symbol, double volume, Nullable<double> price, Nullable<double> stopPrice, double commission, double agentCommission, double swap, Nullable<double> staticMarginRate)
    {
        m_type = type;
        m_side = side;
        new (&m_symbol) std::string(symbol);
        m_volume = volume;
        m_price = price;
        m_stopPrice = stopPrice;
        m_commission = commission;
        m_agentCommission = agentCommission;
        m_swap = swap;
        m_staticMarginRate = staticMarginRate;
    }

#ifdef FX_RESTORE_NEW
    #pragma pop_macro("new")
    #undef FX_RESTORE_NEW
#endif

    void CTradeEntry::Clear()
    {
        m_profit = nullptr;
        m_margin = nullptr;
        m_profitStatus = TradeEntryStatus_NotCalculated;
        m_marginStatus = TradeEntryStatus_NotCalculated;
    }

    void CTradeEntry::SetType(const TradeType newType)
    {
        if ((TradeType_Position != newType) && (TradeType_Limit != newType) && (TradeType_Stop != newType))
        {
            throw runtime_error("Invalid trade type; new value can be Position, Limit or Stop only");
        }
        m_type = newType;
    }

    void CTradeEntry::SetSide(const TradeSide newSide)
    {
        if ((TradeSide_Buy != newSide) && (TradeSide_Sell != newSide))
        {
            throw runtime_error("Invalid trade side; new value can be Buy or Sell only");
        }
        m_side = newSide;
    }

    const char* CTradeEntry::DoGetSymbol() const
    {
        return m_symbol.c_str();
    }

    void CTradeEntry::DoGetSymbol(const char* symbol)
    {
        m_symbol = symbol;
    }

    void CTradeEntry::SetVolume(const double newVolume)
    {
        m_volume = ValidatePositiveOrZeroValue(__FUNCTION__, "newVolume", newVolume);
    }

    void CTradeEntry::SetPrice(const Nullable<double> newPrice)
    {
        if (newPrice.HasValue())
        {
            m_price = ValidatePositiveOrZeroValue(__FUNCTION__, "newPrice", newPrice.Value());
        }
        else
        {
            m_price.Reset();
        }
    }

    void CTradeEntry::SetStopPrice(const Nullable<double> newStopPrice)
    {
        if (newStopPrice.HasValue())
        {
            m_stopPrice = ValidatePositiveOrZeroValue(__FUNCTION__, "newStopPrice", newStopPrice.Value());
        }
        else
        {
            m_stopPrice.Reset();
        }
    }

    void CTradeEntry::SetCommission(const double newCommission)
    {
        m_commission = newCommission;
    }

    void CTradeEntry::SetAgentCommission(const double newAgentCommission)
    {
        m_agentCommission = newAgentCommission;
    }

    void CTradeEntry::SetSwap(const double newSwap)
    {
        m_swap = newSwap;
    }

    void CTradeEntry::SetStaticMarginRate(const Nullable<double> newStaticMarginRate)
    {
        if (newStaticMarginRate.HasValue())
        {
            m_staticMarginRate = ValidatePositiveOrZeroValue(__FUNCTION__, "newStaticMarginRate", newStaticMarginRate.Value());
        }
        else
        {
            m_staticMarginRate.Reset();
        }
    }

    bool CTradeEntry::CaculateProlog()
    {
        if (nullptr == SymbolEntry)
        {
            m_profitStatus = TradeEntryStatus_UnknownSymbol;
            m_marginStatus = TradeEntryStatus_UnknownSymbol;
            return false;
        }
        return true;
    }

    void CTradeEntry::CalculateProfit(const ptrdiff_t zzz, CRates& rates)
    {
        m_profitStatus = DoCalculateProfit(zzz, rates);
    }

    TradeEntryStatus CTradeEntry::DoCalculateProfit(ptrdiff_t zzz, CRates& rates)
    {
        if (TradeType_Position != m_type)
        {
            return TradeEntryStatus_Calculated;
        }
        Nullable<CPriceEntry> quote = rates.TryGetRate(*SymbolEntry);
        if (!quote.HasValue())
        {
            return TradeEntryStatus_OffQuotes;
        }

        double profit = 0;
        if (TradeSide_Buy == m_side)
        {
            profit = (quote.Value().Bid - m_price.Value()) * GetNativeVolume();
        }
        else if (TradeSide_Sell == m_side)
        {
            profit = (m_price.Value() - quote.Value().Ask) * GetNativeVolume();
        }
        else
        {
            throw runtime_error("CTradeEntry::DoCalculateProfit() unknown side");
        }

        Nullable<double> rate = rates.CalculateProfitRate(profit, SymbolEntry->ToIndex, SymbolEntry->FromIndex, zzz);
        if (!rate.HasValue())
        {
            return TradeEntryStatus_OffQuotes;
        }

        m_profit = profit * rate.Value();
        return TradeEntryStatus_Calculated;
    }

    void CTradeEntry::CalculateStaticMargin(double leverageFactor)
    {
        CalculateMargin(leverageFactor, m_staticMarginRate);
    }

    void CTradeEntry::CalculateDynamicMargin(const ptrdiff_t zzz, double leverageFactor, CRates& rates)
    {
        Nullable<double> rate = rates.CalculateMarginRate(m_side, SymbolEntry->ToIndex, SymbolEntry->FromIndex, zzz);
        CalculateMargin(leverageFactor, rate);
    }

    void CTradeEntry::CalculateStaticIfPossibleMargin(const ptrdiff_t zzz, double leverageFactor, CRates& rates)
    {
        CalculateStaticMargin(leverageFactor);
        if (TradeEntryStatus_Calculated != m_marginStatus)
        {
            CalculateDynamicMargin(zzz, leverageFactor, rates);
        }
    }

    void CTradeEntry::CalculateMargin(double leverageFactor, Nullable<double> rate)
    {
        if (rate.HasValue())
        {
            double margin = SymbolEntry->GetMarginFactor() * GetNativeVolume() * rate.Value();

            if (FxMarginCalcMode_Forex == SymbolEntry->GetMarginCalcMode() || FxMarginCalcMode_CfdLeverage == SymbolEntry->GetMarginCalcMode())
                margin /= leverageFactor;

            m_margin = margin;
            m_marginStatus = TradeEntryStatus_Calculated;
            SymbolEntry->CollectMargin(*this);
        }
        else
        {
            m_marginStatus = TradeEntryStatus_OffQuotes;
        }
    }

    double CTradeEntry::GetNativeVolume() const
    {
        double result = m_volume * SymbolEntry->GetContractSize();
        return result;
    }

    std::ostream& operator << (std::ostream& stream, const CTradeEntry& entry)
    {
        Process("Tag", entry.Tag, stream);
        Process("Type", entry.m_type, stream);
        Process("Side", entry.m_side, stream);
        Process("Symbol", entry.m_symbol, stream);
        Process("Price", entry.m_price, stream);
        Process("Volume", entry.m_volume, stream);
        Process("Commission", entry.m_commission, stream);
        Process("AgentCommission", entry.m_agentCommission, stream);
        Process("Swap", entry.m_swap, stream);
        Process("StaticMarginRate", entry.m_staticMarginRate, stream);
        Process("ProfitStatus", entry.m_profitStatus, stream);
        Process("MarginStatus", entry.m_marginStatus, stream);
        Process("Profit", entry.m_profit, stream);
        Process("Margin", entry.m_margin, stream);

        return stream;
    }

    std::istream& operator >> (std::istream& stream, CTradeEntry& entry)
    {
        Process("Tag", entry.Tag, stream);
        Process("Type", entry.m_type, stream);
        Process("Side", entry.m_side, stream);
        Process("Symbol", entry.m_symbol, stream);
        Process("Price", entry.m_price, stream);
        Process("Volume", entry.m_volume, stream);
        Process("Commission", entry.m_commission, stream);
        Process("AgentCommission", entry.m_agentCommission, stream);
        Process("Swap", entry.m_swap, stream);
        Process("StaticMarginRate", entry.m_staticMarginRate, stream);
        Process("ProfitStatus", entry.m_profitStatus, stream);
        Process("MarginStatus", entry.m_marginStatus, stream);
        Process("Profit", entry.m_profit, stream);
        Process("Margin", entry.m_margin, stream);

        return stream;
    }
}