
module.exports = async function (callback, data) {
    const tf = require('@tensorflow/tfjs');
    // Load the binding:
    require('@tensorflow/tfjs-node');  // Use '@tensorflow/tfjs-node-gpu' if running with GPU.

    // Define a model for linear regression.
    const model = tf.sequential();

    const xSize = data.xs.length;
    const xWidth = data.xs[0].length;
    const ySize = data.ys.length;
    const yWidth = 1;

    model.add(tf.layers.dense({ units: 1, inputShape: [xWidth] }));

    // Prepare the model for training: Specify the loss and the optimizer.
    model.compile({ loss: 'meanSquaredError', optimizer: 'sgd' });

    const xs = tf.tensor2d(data.xs, [xSize, xWidth]);
    const ys = tf.tensor2d(data.ys, [ySize, yWidth]);

    // Train the model using the data.
    model.fit(xs, ys, {epochs: 40}).then(() => {
        const savedModel = model.toJSON();
        model.save('file://./my-model-1').then(saved => {
            callback(null, JSON.stringify(savedModel));
        });
    });
    
} 