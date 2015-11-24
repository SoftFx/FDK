using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ArbitrageContext : DbContext
    {
        public DbSet<Arbitrage> Arbitrages { get; set; }
        public DbSet<ArbitrageDetail> ArbitrageDetails { get; set; }
        public DbSet<DoubleArbitrage2B1A> DoubleArbitrage2B1A { get; set; }
        public DbSet<DoubleArbitrage1B2A> DoubleArbitrage1B2A { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arbitrage>()
            .Property(f => f.StartDateTime)
            .HasColumnType("datetime2");

            modelBuilder.Entity<Arbitrage>()
            .Property(f => f.EndDateTime)
            .HasColumnType("datetime2");

            modelBuilder.Entity<ArbitrageDetail>()
            .Property(f => f.StartTickTime)
            .HasColumnType("datetime2");

            modelBuilder.Entity<ArbitrageDetail>()
            .Property(f => f.EndTickTime)
            .HasColumnType("datetime2");

        }
    }

    public class Arbitrage
    {
        public Arbitrage(DateTime dateTimeStart, int firstId, int secondId, string symbol)
        {
            this.StartDateTime = dateTimeStart;
            this.BankBidGAsk = firstId;
            this.BankAskLBid = secondId;
            this.ArbitrageDetails = new List<ArbitrageDetail>();
            this.Symbol = symbol;
        }
        public int ArbitrageId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Double Duration { get; set; }
        public Int32 BankBidGAsk { get; set; }
        public Int32 BankAskLBid { get; set; }

        public List<ArbitrageDetail> ArbitrageDetails { get; set; }
        [MaxLength(20)]
        public String Symbol { get; set; }

        public bool IsFinished
        {
            get
            {
                return Duration > 0;
            }
        }
    }

    public class ArbitrageDetail
    {
        public ArbitrageDetail(DateTime startTime)
        {
            this.StartTickTime = startTime;
        }

        public int ArbitrageDetailId { get; set; }
        public Double B1Ask1 { get; set; }
        public Double B1Ask1Volume { get; set; }
        public Double B1Ask2 { get; set; }
        public Double B1Ask3 { get; set; }
        public Double B1Bid1 { get; set; }
        public Double B1Bid1Volume { get; set; }
        public Double B1Bid2 { get; set; }
        public Double B1Bid3 { get; set; }

        public Double B2Ask1 { get; set; }
        public Double B2Ask1Volume { get; set; }
        public Double B2Ask2 { get; set; }
        public Double B2Ask3 { get; set; }
        public Double B2Bid1 { get; set; }
        public Double B2Bid1Volume { get; set; }
        public Double B2Bid2 { get; set; }
        public Double B2Bid3 { get; set; }

        public Double spread { get; set; }
        public Double duration { get; set; }
        public DateTime StartTickTime { get; set; }
        public DateTime EndTickTime { get; set; }

    }

    public class ArbitrageSpecificDA : Arbitrage
    {
        public ArbitrageSpecificDA(DateTime dateTimeStart, int firstId, int secondId, string symbol)
            : base(dateTimeStart, firstId, secondId, symbol)
        {
        }

        public int ArbitrageSpecificDAId { get; set; }

    }

    public class DoubleArbitrage2B1A
    {
        public DoubleArbitrage2B1A()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public int DoubleArbitrage2B1AId { get; set; }

        public string Symbol { get; set; }
        public Arbitrage FirstArbitrage { get; set; }
        public Arbitrage SecondArbitrage { get; set; }

        //min(ask, b1+b2)
        public double ExpectedAsk { get; set; }
        public double ExpectedAskVolume { get; set; }
        public double ExecutedAsk { get; set; }
        public double ExecutedAskVolume { get; set; }
        
        public double ExecutedBid { get; set; }
        public double ExecutedBidVolume { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime TradeDate { get; set; }


    }
    public class DoubleArbitrage1B2A
    {
        public DoubleArbitrage1B2A()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public int DoubleArbitrage1B2AId { get; set; }

        public string Symbol { get; set; }

        public Arbitrage FirstArbitrage { get; set; }
        public Arbitrage SecondArbitrage { get; set; }

        //min(ask, b1+b2)
        public double ExpectedBid { get; set; }
        public double ExpectedBidVolume { get; set; }
        public double ExecutedBid { get; set; }
        public double ExecutedBidVolume { get; set; }

        public double ExecutedAsk { get; set; }
        public double ExecutedBidAsk { get; set; }
    
        public DateTime CreatedDate { get; set; }
        public DateTime TradeDate { get; set; }

    }


}
