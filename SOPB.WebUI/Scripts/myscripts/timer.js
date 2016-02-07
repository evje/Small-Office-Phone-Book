function clock() {
    var date = new Date();
    document.getElementById("date").innerHTML = date.toLocaleString();
}

function timer() {
    var myVar = setInterval(clock, 1000);
}

    //function checkPagingInput(obj) {
    //	var inp = obj.items.value;
    //	var inppattern = /[123456789]/;
    //	var checking = inppattern.test(inp);
    //	if (checking != true)
    //		alert("Введенные данные некорректны! Введите ЦИФРУ!!!");
    //}

    //window.onload = function highlightActiveMenuItem() {
    //    //$('div.#categories' + ' a').removeClass("active");
    //    //var pageurl = window.location.pathname;
    //    //$('div#categories' + 'a[href="' + pageurl + '"]').addClass("selected");
    //    //var pageHref = window.location.href;
    //    //var linkHref = $("div#categories" + "a").attr('href');
    //    //if (pageHref === linkHref) {
    //    //    var myclass = $("div#categories" + "a").addClass("selected");
    //    //}
//}

    

