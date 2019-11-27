namespace CreditScoreCalculator {
	public interface ICreditCalculator {
		/// <summary>
		/// Calculates the available credit (in $) for a given customer
		/// </summary>
		/// <param name="customer">The customer for whom we are calculating credit</param>
		/// <returns>Available credit amount in $</returns>
		decimal CalculateCredit(Customer customer);

		/// <summary>
		/// Calculates the partial score for our credit model from a bureau score
		/// </summary>
		/// <param name="bureauScore">The score from the credit bureau</param>
		/// <returns>A partial score</returns>
		int CalculateBureauScore(int bureauScore);

		/// <summary>
		/// Calculates the partial score for our credit model from a count of completed payments
		/// </summary>
		/// <param name="completedPayments">Count of completed payments</param>
		/// <returns>A partial score</returns>
		int CalculateCompletedPaymentScore(int completedPayments);

		/// <summary>
		/// Calculates the partial score for our credit model from a count of missed payments
		/// </summary>
		/// <param name="missedPayments">Count of missed payments</param>
		/// <returns>A partial score</returns>
		int CalculateMissedPaymentScore(int missedPayments);

		/// <summary>
		/// Calculates the maximum lendable amount based on the persons age
		/// </summary>
		/// <param name="ageInYears">Age in years</param>
		/// <returns>Maximum allowed credit amount in $</returns>
		decimal CalculateAgeCap(int ageInYears);
	}
}