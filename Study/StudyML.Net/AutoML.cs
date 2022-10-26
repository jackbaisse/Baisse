using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;

namespace StudyML.Net
{
    class AutoML
    {
        public ITransformer PerformBinaryClassification(IDataView trainingData, IDataView validationData)
        {
            // Set up the experiment
            MLContext context = new MLContext();
            uint maxSeconds = 10;
            BinaryClassificationExperiment experiment = context.Auto().CreateBinaryClassificationExperiment(maxSeconds);

            // Run the experiment and wait synchronously for it to complete
            ExperimentResult<BinaryClassificationMetrics> result =
                experiment.Execute(trainingData, validationData, labelColumnName: "ShouldApproveLoan");

            // result.BestRun.ValidationMetrics has properties helpful for evaluating model performance
            double accuracy = result.BestRun.ValidationMetrics.Accuracy;
            double f1Score = result.BestRun.ValidationMetrics.F1Score;
            string confusionTable = result.BestRun.ValidationMetrics.ConfusionMatrix.GetFormattedConfusionTable();

            // Return the best performing trained model
            ITransformer bestModel = result.BestRun.Model;
            return bestModel;
        }

        //public LoanPrediction PredictBinaryClassification(ITransformer bestModel, IDataView trainingData, LoanData loan)
        //{
        //    MLContext context = new MLContext();

        //    // Create an engine capable of evaluating one or more loans in the future
        //    PredictionEngine<LoanData, LoanPrediction> engine =
        //        context.Model.CreatePredictionEngine<LoanData, LoanPrediction>(bestModel, trainingData.Schema);

        //    // Actually make the prediction and return the findings
        //    LoanPrediction prediction = engine.Predict(loan);
        //    return prediction;
        //}


        public ITransformer PerformMultiClassification(IDataView trainingData, IDataView validationData)
        {
            // Set up the experiment
            MLContext context = new MLContext();
            uint maxSeconds = 10;
            MulticlassClassificationExperiment experiment = context.Auto().CreateMulticlassClassificationExperiment(maxSeconds);

            // Run the experiment and wait synchronously for it to complete
            ExperimentResult<MulticlassClassificationMetrics> result =
                experiment.Execute(trainingData, validationData, labelColumnName: "RiskCategory");

            // result.BestRun.ValidationMetrics has properties helpful for evaluating model performance
            string confusionTable = result.BestRun.ValidationMetrics.ConfusionMatrix.GetFormattedConfusionTable();

            // Return the best performing trained model
            ITransformer bestModel = result.BestRun.Model;
            return bestModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingData"></param>
        /// <param name="validationData"></param>
        /// <returns></returns>

        public ITransformer PerformRecommendation(IDataView trainingData, IDataView validationData)
        {
            // Set up the experiment
            MLContext context = new MLContext();
            uint maxSeconds = 10;
            RecommendationExperiment experiment = context.Auto().CreateRecommendationExperiment(maxSeconds);

            // Run the experiment and wait synchronously for it to complete
            ExperimentResult<RegressionMetrics> result =
                experiment.Execute(trainingData, validationData, labelColumnName: "Rating");

            // result.BestRun.ValidationMetrics has properties helpful for evaluating model performance
            double error = result.BestRun.ValidationMetrics.MeanAbsoluteError;

            // Return the best performing trained model
            ITransformer bestModel = result.BestRun.Model;
            return bestModel;
        }


        public ITransformer PerformRanking(IDataView trainingData, IDataView validationData)
        {
            // Set up the experiment
            MLContext context = new MLContext();
            uint maxSeconds = 10;
            RankingExperiment experiment = context.Auto().CreateRankingExperiment(maxSeconds);

            // Run the experiment and wait synchronously for it to complete
            ExperimentResult<RankingMetrics> result =
                experiment.Execute(trainingData, validationData, labelColumnName: "Temperature");

            // result.BestRun.ValidationMetrics has properties helpful for evaluating model performance
            IEnumerable<double> gains = result.BestRun.ValidationMetrics.DiscountedCumulativeGains;
            IEnumerable<double> normalizedGains = result.BestRun.ValidationMetrics.NormalizedDiscountedCumulativeGains;

            // Return the best performing trained model
            ITransformer bestModel = result.BestRun.Model;

            RankingEvaluatorOptions options = new RankingEvaluatorOptions();
            RankingMetrics metrics = context.Ranking.Evaluate(trainingData, labelColumnName: "Label", rowGroupColumnName: "Group", scoreColumnName: "Score");
            return bestModel;
        }
    }
}
