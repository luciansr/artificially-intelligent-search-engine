module.exports = async function (callback, object1) {
    const tf = require('@tensorflow/tfjs');
    // Load the binding:
    require('@tensorflow/tfjs-node');  // Use '@tensorflow/tfjs-node-gpu' if running with GPU.

    // Define a model for linear regression.
    const model = tf.sequential();
    model.add(tf.layers.dense({ units: 1, inputShape: [1] }));

    // Prepare the model for training: Specify the loss and the optimizer.
    model.compile({ loss: 'meanSquaredError', optimizer: 'sgd' });

    // Generate some synthetic data for training.
    const xs = tf.tensor2d([1, 2, 3, 4], [4, 1]);
    const ys = tf.tensor2d([1, 2, 3, 4], [4, 1]);

    // Train the model using the data.
    model.fit(xs, ys, {epochs: 40}).then(() => {
        // Use the model to do inference on a data point the model hasn't seen before:
        // Open the browser devtools to see the output
        model.predict(tf.tensor2d([4], [1, 1])).print();

        model.predict(tf.tensor2d([1,2,3,4], [4, 1])).print();

        callback(null, object1);
    });
    
} 