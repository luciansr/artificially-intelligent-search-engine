module.exports = async function (callback, query, data, savedModel) {
    const tf = require('@tensorflow/tfjs');
    // Load the binding:
    require('@tensorflow/tfjs-node'); 
    const folderName = query.replace(/[^a-zA-Z0-9]/gi, '');
    const fullFolderName = 'file://./LearnedWeights/' + folderName + '/model.json';

    // tf.models.modelFromJSON(savedModel).then(model => {
    //     const size = data.xs.length;
    //     const width = data.xs[0].length;
    //     const xs = tf.tensor2d(data.xs, [size, width]);
    //     // Predict the data using the model.
    //     // model.predict(xs).print();
    //     result = model.predict(xs);

    //     callback(null, result);
    // });

    const model = await tf.loadModel(fullFolderName);
    const size = data.xs.length;
    const width = data.xs[0].length;
    const xs = tf.tensor2d(data.xs, [size, width]);
    // Predict the data using the model.
    // model.predict(xs).print();
    xs.print();
    result = model.predict(xs).as1D();
    console.log('teste');

    callback(null, Array.from(result.dataSync()));
} 