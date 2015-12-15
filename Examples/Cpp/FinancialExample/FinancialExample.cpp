#include "stdafx.h"
#include "QuotesResolver.h"


int main()
{
	// create quotes resolver, which allows to handle price resolve requests
	CQuotesResolver resolver;

	// create financial calculator
	CFinancialCalculator calculator(&CQuotesResolver::ResolvePrice, &resolver);
	calculator.SetMarginMode(MarginMode_Dynamic);

	// adding major currencies
	calculator.Currencies.push_back("USD");
	calculator.Currencies.push_back("EUR");
	calculator.Currencies.push_back("GBP");
	calculator.Currencies.push_back("CHF");
	calculator.Currencies.push_back("JPY");


	// creates symbols
	CSymbolEntry chfusd("CHFUSD", "USD", "CHF");
	CSymbolEntry eurusd("EURUSD", "USD", "EUR");
	CSymbolEntry eurjpy("EURJPY", "JPY", "EUR");
	CSymbolEntry usdjpy("USDJPY", "JPY", "USD");
	usdjpy.SetHedging(1);

	// you may not specify contract size,
	// in this case you should provide volume of trades in natural units
	chfusd.SetContractSize(100000);
	eurusd.SetContractSize(100000);
	eurjpy.SetContractSize(100000);
	usdjpy.SetContractSize(100000);
	

	// adding symbols
	calculator.Symbols.insert(chfusd);
	calculator.Symbols.insert(eurusd);
	calculator.Symbols.insert(eurjpy);
	calculator.Symbols.insert(usdjpy);

	// adding quotes
	calculator.Prices.Update("CHFUSD", 1.12081, 1.12082);

	// create account
	CAccountEntry net;
	net.Tag.SetData<int>(100); // specify your custom data, for example account id
	net.SetAccountType(AccountType_Net);
	net.SetCurrency("USD");
	net.SetLeverage(100);
	net.SetBalance(5000);

	// create trades

	CTradeEntry position;
	position.Tag.SetData<int>(3000); // specify your custom data, for example order id
	position.SetType(TradeType_Position);
	position.SetSide(TradeSide_Buy);
	position.SetVolume(0.11);
	position.SetSymbol("EURUSD");
	position.SetPrice(1.36);

	// adding trades
	net.Trades.push_back(position);

	// adding accounts
	calculator.Accounts.push_back(net);

	// calculate
	calculator.Calculate();


	// print result

	for each(const auto& account in calculator.Accounts)
	{
		cout<<"account #"<<account.Tag.GetData<int>()<<endl;
		cout<<"\tprofit status = "<<account.GetProfitStatus()<<endl;
		cout<<"\tmargin status = "<<account.GetMarginStatus()<<endl;
		cout<<"\tprofit = "<<account.GetProfit()<<endl;
		cout<<"\tmargin = "<<account.GetMargin()<<endl;
		cout<<endl;

		for each(const auto& trade in account.Trades)
		{
			cout<<"\ttrade #"<<trade.Tag.GetData<int>()<<endl;
			cout<<"\t\tprofit status = "<<trade.GetProfitStatus()<<endl;
			cout<<"\t\tmargin status = "<<trade.GetMarginStatus()<<endl;
			cout<<"\t\tprofit = "<<trade.GetProfit()<<endl;
			cout<<"\t\tmargin = "<<trade.GetMargin()<<endl;
			cout<<endl;
		}
	}
	return 0;
}

