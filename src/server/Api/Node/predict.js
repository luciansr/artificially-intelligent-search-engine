module.exports = async function (callback, savedModel, data) {
    const tf = require('@tensorflow/tfjs');
    // Load the binding:
    require('@tensorflow/tfjs-node'); 

    tf.models.modelFromJSON(savedModel).then(model => {
        const size = data.xs.length;
        const width = data.xs[0].length;
        const xs = tf.tensor2d(data.xs, [size, width]);
        // Predict the data using the model.
        // model.predict(xs).print();
        result = model.predict(xs);

        callback(null, result);
    });


} 