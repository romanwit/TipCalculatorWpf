using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace TipCalculatorWpf.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        private MainViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new MainViewModel();
        }

        [Test]
        public void Records_InitiallyEmpty()
        {
            Assert.That(_viewModel.Records != null);
            Assert.That(_viewModel.Records, Is.Empty);
        }

        [Test]
        public void TotalAmount_InitiallyZero()
        {
            Assert.That(_viewModel.TotalAmount == 0.0m);
        }

        [Test]
        public void AddRecordCommand_AddsNewRecordToRecords()
        {
            Assert.That(_viewModel.Records, Is.Empty);

            _viewModel.AddRecordCommand.Execute(null);

            Assert.That(_viewModel.Records.Count==1);
            //Assert.IsInstanceOf<TipRecord>(_viewModel.Records.First());
        }

        [Test]
        public void TotalAmount_UpdatesWhenRecordAdded()
        {
            var initialTotal = _viewModel.TotalAmount;
            _viewModel.AddRecordCommand.Execute(null);

            _viewModel.Records[0].Amount = 100;
            _viewModel.Records[0].Tip = 10;

            Assert.That(_viewModel.TotalAmount== 110.0m);
        }

        [Test]
        public void TotalAmount_UpdatesWhenRecordModified()
        {
            _viewModel.AddRecordCommand.Execute(null);
            _viewModel.Records[0].Amount = 100;
            _viewModel.Records[0].Tip = 10;

            Assert.That(_viewModel.TotalAmount== 110.0m);

            _viewModel.Records[0].Tip = 20;
            Assert.That(_viewModel.TotalAmount== 120.0m);
        }

        [Test]
        public void PropertyChanged_EventRaisedForTotalAmountWhenRecordAdded()
        {
            var wasCalled = false;
            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MainViewModel.TotalAmount))
                    wasCalled = true;
            };

            _viewModel.AddRecordCommand.Execute(null);

            Assert.That(wasCalled);
        }

        [Test]
        public void PropertyChanged_EventRaisedForTotalAmountWhenRecordModified()
        {
            _viewModel.AddRecordCommand.Execute(null);
            var wasCalled = false;
            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MainViewModel.TotalAmount))
                    wasCalled = true;
            };

            _viewModel.Records[0].Amount = 200;

            Assert.That(wasCalled);
        }
    }
}

