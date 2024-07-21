function displayImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            // Get the uploaded image data URL
            var imageDataUrl = e.target.result;

            // Get a reference to the image element
            var imageElement = document.getElementById('outputImage');

            // Set the image element's src attribute to the data URL
            imageElement.src = imageDataUrl;
        }

        reader.readAsDataURL(input.files[0]);
    }
}