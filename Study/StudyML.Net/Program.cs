using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace StudyML.Net
{
    class Program
    {
        public class HouseData
        {
            public float Size { get; set; }
            public float Price { get; set; }
        }

        /// <summary>
        /// 所有算法在执行预测后还会创建新列。 这些新列的固定名称取决于机器学习算法的类型。 对于回归任务，其中一个新列称为分数。 这就是我们将价格数据归为此名称的原因。
        /// </summary>
        public class Prediction
        {
            [ColumnName("Score")]
            public float Price { get; set; }
        }

        static void Main(string[] args)
        {
            SimilarPhoto photo = new SimilarPhoto("");
            string a = photo.GetHash();

            SimilarPhoto photo1 = new SimilarPhoto("");
            string b = photo1.GetHash();

            MLContext mlContext = new MLContext();

            // 1. 导入或创建培训数据
            HouseData[] houseData = {
               new HouseData() { Size = 1.1F, Price = 1.2F },
               new HouseData() { Size = 1.9F, Price = 2.3F },
               new HouseData() { Size = 2.8F, Price = 3.0F },
               new HouseData() { Size = 3.4F, Price = 3.7F } };
            IDataView trainingData = mlContext.Data.LoadFromEnumerable(houseData);

            // 2. 指定数据准备和模型训练管道
            //生成管道
            //每个目录中都有一组扩展方法。 让我们看看如何使用扩展方法创建训练管道。
            var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

            // 3. 定型模型
            //在管道中创建对象后，即可使用数据来训练模型。
            //调用 Fit() 使用输入训练数据来估算模型的参数。 这称为训练模型。 请记住，上述线性回归模型有两个模型参数：偏差和权重。 在 Fit() 调用后，参数的值是已知的。 大部分模型拥有的参数比这多得多。
            var model = pipeline.Fit(trainingData);

            // 4. 使用模型
            //可以将输入数据批量转换为预测，也可以一次转换一个输入。 在房屋价格示例中，我们同时执行了两种操作：为了评估模型而执行批量转换，以及为了进行新预测而执行单次转换。 让我们进行单个预测。
            //CreatePredictionEngine() 方法接受一个输入类和一个输出类。 字段名称和/或代码属性确定模型训练和预测期间使用的数据列的名称。
            var size = new HouseData() { Size = 2.5F };
            var price = mlContext.Model.CreatePredictionEngine<HouseData, Prediction>(model).Predict(size);

            Console.WriteLine($"Predicted price for size: {size.Size * 1000} sq ft= {price.Price * 100:C}k");

            // Predicted price for size: 2500 sq ft= $261.98k

            #region 模型
            //模型数据
            HouseData[] testHouseData =
            {
             new HouseData() { Size = 1.1F, Price = 0.98F },
            new HouseData() { Size = 1.9F, Price = 2.1F },
                 new HouseData() { Size = 2.8F, Price = 2.9F },
            new HouseData() { Size = 3.4F, Price = 3.6F }
             };

            var testHouseDataView = mlContext.Data.LoadFromEnumerable(testHouseData);

            //模型将输入数据转换为预测。
            var testPriceDataView = model.Transform(testHouseDataView);

            var metrics = mlContext.Regression.Evaluate(testPriceDataView, labelColumnName: "Price");

            Console.WriteLine($"R^2: {metrics.RSquared:0.##}");
            Console.WriteLine($"RMS error: {metrics.RootMeanSquaredError:0.##}");

            //DataView 对象的一个重要属性是它们被惰性求值。 数据视图仅在模型训练和评估以及数据预测期间加载及运行。 在编写和测试 ML.NET 应用程序时，可以使用 Visual Studio 调试程序通过调用 Preview 方法来浏览任何数据视图对象。
            //可以在调试程序中查看 debug 变量并检查其内容。 不要在生产代码中使用 Preview 方法，因为它会大幅降低性能。
            var debug = testPriceDataView.Preview();

            //模型部署
            //在实际应用程序中，模型训练和评估代码将与预测分离。 事实上，这两项活动通常由单独的团队执行。 模型开发团队可以保存模型以便用于预测应用程序。
            mlContext.Model.Save(model, trainingData.Schema, "model.zip");

            // R^2: 0.96
            // RMS error: 0.19
            #endregion
        }
    }
}
