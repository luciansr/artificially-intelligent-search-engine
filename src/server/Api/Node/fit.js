
module.exports = async function (callback, data) {
    const tf = require('@tensorflow/tfjs');
    // Load the binding:
    require('@tensorflow/tfjs-node');  // Use '@tensorflow/tfjs-node-gpu' if running with GPU.

    // Define a model for linear regression.
    const model = tf.sequential();
    model.add(tf.layers.dense({ units: 1, inputShape: [1] }));

    // Prepare the model for training: Specify the loss and the optimizer.
    model.compile({ loss: 'meanSquaredError', optimizer: 'sgd' });

    const xSize = data.xs.length;
    const xWidth = data.xs[0].length;
    const xs = tf.tensor2d(data.xs, [xSize, xWidth]);

    const ySize = data.ys.length;
    const ySize = data.ys[0].length;
    const ys = tf.tensor2d(data.ys, [ySize, ySize]);

    // Train the model using the data.
    model.fit(xs, ys, {epochs: 40}).then(() => {
        const savedModel = model.toJSON();
        callback(null, savedModel);
    });
    
} 