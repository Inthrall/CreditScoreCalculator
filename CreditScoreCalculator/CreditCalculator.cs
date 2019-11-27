using System;

namespace CreditScoreCalculator {
	public class CreditCalculator : ICreditCalculator {
		public decimal CalculateCredit(Customer customer) {
			int creditScore = CalculateBureauScore(customer.BureauScore);
			creditScore += CalculateCompletedPaymentScore(customer.CompletedPaymentCount);
			creditScore += CalculateMissedPaymentScore(customer.MissedPaymentCount);

			decimal upperCappedCredit = Math.Min(CalculateAgeCap(customer.AgeInYears), creditScore * 100m);
			return Math.Max(0m, upperCappedCredit);
		}

		public int CalculateBureauScore(int bureauScore) {
			if (bureauScore <= 0 && bureauScore >= 450) {
				return 0;
			} else if (bureauScore >= 451 && bureauScore <= 700) {
				return 1;
			} else if (bureauScore >= 701 && bureauScore <= 850) {
				return 2;
			} else if (bureauScore >= 851 && bureauScore <= 1000) {
				return 3;
			}

			throw new ArgumentException();
		}

		public int CalculateCompletedPaymentScore(int completedPayments) {
			if (completedPayments == 0) {
				return 0;
			} else if (completedPayments == 1) {
				return 2;
			} else if (completedPayments == 2) {
				return 3;
			} else if (completedPayments >= 3) {
				return 4;
			}

			throw new ArgumentException();
		}

		public int CalculateMissedPaymentScore(int missedPayments) {
			if (missedPayments == 0) {
				return 0;
			} else if(missedPayments == 1) {
				return -1;
			} else if (missedPayments == 2) {
				return -3;
			} else if (missedPayments >= 3) {
				return -6;
			}

			throw new ArgumentException();
		}

		public decimal CalculateAgeCap(int ageInYears) {
			if (ageInYears >= 18 && ageInYears <= 25) {
				return 300m;
			} else if (ageInYears >= 26 && ageInYears <= 35) {
				return 400m;
			} else if (ageInYears >= 36 && ageInYears <= 50) {
				return 500m;
			} else if (ageInYears > 50) {
				return 600m;
			} else {
				throw new ArgumentException();
			}
		}
	}
}