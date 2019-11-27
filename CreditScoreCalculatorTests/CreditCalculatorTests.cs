using CreditScoreCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CreditScoreCalculatorTests {
	[TestClass]
	public class CreditCalculatorTests {
		private ICreditCalculator _creditCalculator;

		[TestInitialize]
		public void Setup() {
			_creditCalculator = new CreditCalculator();
		}

		[TestMethod]
		public void OverallCreditTest() {
			Customer john = new Customer(750, 1, 4, 29);
			Assert.AreEqual(400m, _creditCalculator.CalculateCredit(john));
		}

		[TestMethod]
		public void UpperLimitCreditTest() {
			Customer eugene = new Customer(1000, 0, 500, 90);
			Assert.AreEqual(600m, _creditCalculator.CalculateCredit(eugene));
		}

		[TestMethod]
		public void LowerLimitCreditTest() {
			Customer eugene = new Customer(451, 500, 0, 18);
			Assert.AreEqual(0m, _creditCalculator.CalculateCredit(eugene));
		}

		[TestMethod]
		public void BureauScoreTest() {
			// Low Bureau Scores
			foreach (int score in new List<int>() { 451, 700 }) {
				Assert.AreEqual(1, _creditCalculator.CalculateBureauScore(score));
			}

			// Med Bureau Scores
			foreach (int score in new List<int>() { 701, 850 }) {
				Assert.AreEqual(2, _creditCalculator.CalculateBureauScore(score));
			}

			// High Bureau Scores
			foreach (int score in new List<int>() { 851, 1000 }) {
				Assert.AreEqual(3, _creditCalculator.CalculateBureauScore(score));
			}

			// Invalid Bureau Scores
			foreach (int score in new List<int>() { -1, 0, 450, 1001, int.MinValue, int.MaxValue }) {
				try {
					_creditCalculator.CalculateBureauScore(score);
				} catch (ArgumentException) {
				} catch (Exception) {
					Assert.Fail();
				}
			}
		}

		[TestMethod]
		public void CompletedPaymentsTest() {
			Assert.AreEqual(0m, _creditCalculator.CalculateCompletedPaymentScore(0));
			Assert.AreEqual(2m, _creditCalculator.CalculateCompletedPaymentScore(1));
			Assert.AreEqual(3m, _creditCalculator.CalculateCompletedPaymentScore(2));

			foreach (int payment in new List<int>() { 3, 4, 1000, int.MaxValue }) {
				Assert.AreEqual(4m, _creditCalculator.CalculateCompletedPaymentScore(payment));
			}

			foreach (int payment in new List<int>() { -1, int.MinValue }) {
				try {
					_creditCalculator.CalculateCompletedPaymentScore(payment);
				} catch (ArgumentException) {
				} catch (Exception) {
					Assert.Fail();
				}
			}
		}

		[TestMethod]
		public void MissedPaymentsTest() {
			Assert.AreEqual(0, _creditCalculator.CalculateMissedPaymentScore(0));
			Assert.AreEqual(-1, _creditCalculator.CalculateMissedPaymentScore(1));
			Assert.AreEqual(-3, _creditCalculator.CalculateMissedPaymentScore(2));

			// Lots of payments
			foreach (int payment in new List<int>() { 3, 1000, int.MaxValue }) {
				Assert.AreEqual(-6, _creditCalculator.CalculateMissedPaymentScore(payment));
			}

			// Invalid Credit Scores
			foreach (int payment in new List<int>() { -1, int.MinValue }) {
				try {
					_creditCalculator.CalculateMissedPaymentScore(payment);
				} catch (ArgumentException) {
				} catch (Exception) {
					Assert.Fail();
				}
			}
		}

		[TestMethod]
		public void AgeCapTest() {
			// Young adult
			foreach (int age in new List<int>() { 18, 22, 25 }) {
				Assert.AreEqual(300m, _creditCalculator.CalculateAgeCap(age));
			}

			// Adult
			foreach (int age in new List<int>() { 26, 30, 35 }) {
				Assert.AreEqual(400m, _creditCalculator.CalculateAgeCap(age));
			}

			// Middle age
			foreach (int age in new List<int>() { 36, 45, 50 }) {
				Assert.AreEqual(500m, _creditCalculator.CalculateAgeCap(age));
			}

			// Old age
			foreach (int age in new List<int>() { 51, 61, 120, int.MaxValue }) {
				Assert.AreEqual(600m, _creditCalculator.CalculateAgeCap(age));
			}

			// Invalid ages
			foreach (int age in new List<int>() { int.MinValue, -1, 0, 5, 17 }) {
				try {
					_creditCalculator.CalculateAgeCap(age);
				} catch (ArgumentException) {
				} catch (Exception) {
					Assert.Fail();
				}
			}
		}

	}
}