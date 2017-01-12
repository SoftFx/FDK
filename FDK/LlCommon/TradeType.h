#pragma once

namespace FDK
{
    __declspec(align(4)) enum TradeType
    {
        TradeType_None = -1,
        TradeType_Position = 0,
        TradeType_Limit = 1,
        TradeType_Stop = 2,
        TradeType_StopLimit = 3,
    };


    inline std::ostream& operator << (std::ostream& stream, TradeType argument)
    {
        if (TradeType_None == argument)
        {
            stream << "None";
        }
        else if (TradeType_Position == argument)
        {
            stream << "Position";
        }
        else if (TradeType_Limit == argument)
        {
            stream << "Limit";
        }
        else if (TradeType_Stop == argument)
        {
            stream << "Stop";
        }
        else if (TradeType_StopLimit == argument)
        {
            stream << "StopLimit";
        }
        else
        {
            stream << static_cast<int>(argument);
        }
        return stream;
    }
}