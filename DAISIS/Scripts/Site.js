let param = window.location.hash.substr(1);
if (param !== '') {
    let navItem = $('.card-header a[href="#' + param + '"]');
    if (navItem.length) {
        $('.card-header .nav-link').removeClass('active');
        navItem.addClass('active');
    }
    
    let container = $('#' + param);
    if (container.length) {
        $('.container').removeClass('active');
        container.addClass('active');
    }
}