using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using System.ComponentModel;

namespace TipCalculatorWpf.Tests
{
    [TestFixture]
    public class TipRecordTests
    {
        private TipRecord _tipRecord;

        [SetUp]
        public void Setup()
        {
            _tipRecord = new TipRecord();
        }

        [Test]
        public void Total_CalculatesCorrectly()
        {
            _tipRecord.Amount = 100;
            _tipRecord.Tip = 15;
            Assert.That(115.00m == _tipRecord.Total);
            
        }

        [Test]
        public void Total_UpdatesWhenAmountChanges()
        {
            _tipRecord.Amount = 200;
            _tipRecord.Tip = 10;

            Assert.That(220.00m == _tipRecord.Total);

            _tipRecord.Amount = 150;
            Assert.That(165.00m == _tipRecord.Total);
        }

        [Test]
        public void Total_UpdatesWhenTipChanges()
        {
            _tipRecord.Amount = 100;
            _tipRecord.Tip = 20;

            Assert.That(120.00m == _tipRecord.Total);

            _tipRecord.Tip = 25;
            Assert.That(125.00m == _tipRecord.Total);
        }

        [Test]
        public void PropertyChanged_EventRaisedForAmount()
        {
            var wasCalled = false;
            _tipRecord.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TipRecord.Amount))
                    wasCalled = true;
            };

            _tipRecord.Amount = 100;

            Assert.That(wasCalled);
        }

        [Test]
        public void PropertyChanged_EventRaisedForTip()
        {
            var wasCalled = false;
            _tipRecord.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TipRecord.Tip))
                    wasCalled = true;
            };

            _tipRecord.Tip = 15;

            Assert.That(wasCalled);
        }

        [Test]
        public void PropertyChanged_EventRaisedForTotalWhenAmountChanges()
        {
            var wasCalled = false;
            _tipRecord.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TipRecord.Total))
                    wasCalled = true;
            };

            _tipRecord.Amount = 100;

            Assert.That(wasCalled);
        }

        [Test]
        public void PropertyChanged_EventRaisedForTotalWhenTipChanges()
        {
            var wasCalled = false;
            _tipRecord.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TipRecord.Total))
                    wasCalled = true;
            };

            _tipRecord.Tip = 20;

            Assert.That(wasCalled);
        }
    }
}

