function convertToSlug() {
    var title = document.getElementById('Title').value;
    var result = title.toLowerCase().replace(/\s/g, '-').replace(/[^\w-]+/g, '');
    document.getElementById('UrlSlug').value = result;
}