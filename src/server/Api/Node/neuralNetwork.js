module.exports = async function (callback, object1) {
    const tf = require('@tensorflow/tfjs');
    // Load the binding:
    require('@tensorflow/tfjs-node');  // Use '@tensorflow/tfjs-node-gpu' if running with GPU.

    // Train a simple model:
    // const model = tf.sequential();
    // model.add(tf.layers.dense({units: 100, activation: 'relu', inputShape: [10]}));
    // model.add(tf.layers.dense({units: 1, activation: 'linear'}));
    // model.compile({optimizer: 'sgd', loss: 'meanSquaredError'});

    // const xs = tf.randomNormal([100, 10]);
    // const ys = tf.randomNormal([100, 1]);

    // model.fit(xs, ys, {
    //     epochs: 100,
    //     callbacks: {
    //         onEpochEnd: async (epoch, log) => {
    //         console.log(`Epoch ${epoch}: loss = ${log.loss}`);
    //         }
    //     }
    // });


    /**
 * We want to learn the coefficients that give correct solutions to the
 * following cubic equation:
 *      y = a * x^3 + b * x^2 + c * x + d
 * In other words we want to learn values for:
 *      a
 *      b
 *      c
 *      d
 * Such that this function produces 'desired outputs' for y when provided
 * with x. We will provide some examples of 'xs' and 'ys' to allow this model
 * to learn what we mean by desired outputs and then use it to produce new
 * values of y that fit the curve implied by our example.
 */

    // Step 1. Set up variables, these are the things we want the model
    // to learn in order to do prediction accurately. We will initialize
    // them with random values.
    const a = tf.variable(tf.scalar(Math.random()));
    const b = tf.variable(tf.scalar(Math.random()));
    const c = tf.variable(tf.scalar(Math.random()));
    const d = tf.variable(tf.scalar(Math.random()));


    // Step 2. Create an optimizer, we will use this later. You can play
    // with some of these values to see how the model performs.
    const numIterations = 75;
    const learningRate = 0.5;
    const optimizer = tf.train.sgd(learningRate);

    // Step 3. Write our training process functions.

    /*
     * This function represents our 'model'. Given an input 'x' it will try and
     * predict the appropriate output 'y'.
     *
     * It is also sometimes referred to as the 'forward' step of our training
     * process. Though we will use the same function for predictions later.
     *
     * @return number predicted y value
     */
    function predict(x) {
        // y = a * x ^ 3 + b * x ^ 2 + c * x + d
        return tf.tidy(() => {
            return a.mul(x.pow(tf.scalar(3, 'int32')))
                .add(b.mul(x.square()))
                .add(c.mul(x))
                .add(d);
        });
    }

    function generateData(numPoints, coeff, sigma = 0.04) {
        return tf.tidy(() => {
          const [a, b, c, d] = [
            tf.scalar(coeff.a), tf.scalar(coeff.b), tf.scalar(coeff.c),
            tf.scalar(coeff.d)
          ];
      
          const xs = tf.randomUniform([numPoints], -1, 1);
      
          // Generate polynomial data
          const three = tf.scalar(3, 'int32');
          const ys = a.mul(xs.pow(three))
            .add(b.mul(xs.square()))
            .add(c.mul(xs))
            .add(d)
            // Add random noise to the generated data
            // to make the problem a bit more interesting
            .add(tf.randomNormal([numPoints], 0, sigma));
      
          // Normalize the y values to the range 0 to 1.
          const ymin = ys.min();
          const ymax = ys.max();
          const yrange = ymax.sub(ymin);
          const ysNormalized = ys.sub(ymin).div(yrange);
      
          return {
            xs, 
            ys: ysNormalized
          };
        })
      }

    /*
     * This will tell us how good the 'prediction' is given what we actually
     * expected.
     *
     * prediction is a tensor with our predicted y values.
     * labels is a tensor with the y values the model should have predicted.
     */
    function loss(prediction, labels) {
        // Having a good error function is key for training a machine learning model
        const error = prediction.sub(labels).square().mean();
        return error;
    }

    /*
     * This will iteratively train our model.
     *
     * xs - training data x values
     * ys — training data y values
     */
    async function train(xs, ys, numIterations) {
        for (let iter = 0; iter < numIterations; iter++) {
            // optimizer.minimize is where the training happens.

            // The function it takes must return a numerical estimate (i.e. loss)
            // of how well we are doing using the current state of
            // the variables we created at the start.

            // This optimizer does the 'backward' step of our training process
            // updating variables defined previously in order to minimize the
            // loss.
            optimizer.minimize(() => {
                // Feed the examples into the model
                const pred = predict(xs);
                return loss(pred, ys);
            });

            // Use tf.nextFrame to not block the browser.
            await tf.nextFrame();
        }
    }

    async function learnCoefficients() {
        const trueCoefficients = { a: -.8, b: -.2, c: .9, d: .5 };
        const trainingData = generateData(100, trueCoefficients);

        // Plot original data
        // renderCoefficients('#data .coeff', trueCoefficients);
        // await plotData('#data .plot', trainingData.xs, trainingData.ys)

        // See what the predictions look like with random coefficients
        // renderCoefficients('#random .coeff', {
        //     a: a.dataSync()[0],
        //     b: b.dataSync()[0],
        //     c: c.dataSync()[0],
        //     d: d.dataSync()[0],
        // });
        const predictionsBefore = predict(trainingData.xs);
        // await plotDataAndPredictions(
        //     '#random .plot', trainingData.xs, trainingData.ys, predictionsBefore);

        // Train the model!
        await train(trainingData.xs, trainingData.ys, numIterations);

        // See what the final results predictions are after training.
        // renderCoefficients('#trained .coeff', {
        //     a: a.dataSync()[0],
        //     b: b.dataSync()[0],
        //     c: c.dataSync()[0],
        //     d: d.dataSync()[0],
        // });
        const predictionsAfter = predict(trainingData.xs);
        // await plotDataAndPredictions(
        //     '#trained .plot', trainingData.xs, trainingData.ys, predictionsAfter);

        predictionsBefore.dispose();
        predictionsAfter.dispose();
    }


    await learnCoefficients();

    callback(null, object1);
} 