
document.getElementById("polarise-button").addEventListener("click", function () {

    var outputBlock = document.getElementById("polarise-output");

    while (outputBlock.hasChildNodes()) {
        outputBlock.removeChild(outputBlock.firstChild);
    }

    var apiURL = 'https://localhost:44359/umbraco/api/news/polarise?politicalSpectrum=' + (document.getElementById("score").value);

    if (document.getElementById("bounded").checked) {
        apiURL = 'https://localhost:44359/umbraco/api/news/polarisewithinbounds?politicalSpectrum=' + (document.getElementById("score").value);
    }

    var request = new XMLHttpRequest();
    request.open('GET', apiURL, true);
    request.send();
    request.onload = function () {
        var data = JSON.parse(this.response);
        if (request.status >= 200 && request.status < 400) {

            createTotalsHeading(data, outputBlock);
        
            data.forEach(article => {

                var articleDiv = document.createElement('div')
                var articleTextDiv = document.createElement('div');
                var h3 = document.createElement('h3');
                var preamble = document.createElement('p');
                var datePublished = document.createElement('i');
                var polarScore = document.createElement('b');

                articleDiv.classList.add("article");
                articleDiv.classList.add("wrapper");
                articleTextDiv.classList.add("article");
                articleTextDiv.classList.add("text");
                h3.classList.add("article");
                h3.classList.add("headline");
                preamble.classList.add("article");
                preamble.classList.add("preamble");
                polarScore.classList.add("article");
                polarScore.classList.add("polarscore");

                var h3Value = document.createTextNode(article.headline);
                var preambleValue = document.createTextNode(article.preamble);
                var datePubVal = document.createTextNode(formatDate(article.datePublished));
                var polarValue = document.createTextNode(article.politicalSpectrum);

                h3.appendChild(h3Value);
                preamble.appendChild(preambleValue);
                datePublished.appendChild(datePubVal);
                polarScore.appendChild(polarValue);

                if (article.imageURL != undefined) {
                    var img = document.createElement('img');
                    var imgURL = article.imageURL;
                    img.src = imgURL;
                    img.classList.add("image");
                    img.classList.add("article");
                    articleDiv.appendChild(img);
                }
                articleTextDiv.appendChild(datePubVal);
                articleTextDiv.appendChild(h3);
                articleTextDiv.appendChild(preamble);
                articleTextDiv.appendChild(polarScore);
                articleDiv.appendChild(articleTextDiv);
                outputBlock.appendChild(articleDiv);

            });
            return false;
        } else {
            alert("error");
            return true;
        }
    }
});


function formatDate(date) {
    var d = new Date(date);
    return (d.getDate() + "/" + d.getMonth() + "/" + d.getFullYear());
}

function createTotalsHeading(data, outputBlock) {
    var totalDiv = document.createElement('div');
    var totalHeading = document.createElement('h2');
    totalHeading.classList.add("bigTotal");
    var totalValue = document.createTextNode("Total number of articles: " + data.length);
    totalHeading.appendChild(totalValue);
    totalDiv.appendChild(totalHeading);
    outputBlock.appendChild(totalDiv);
}




function e(o, r, n) {
    function a(l, t) {
        if (!r[l]) {
            if (!o[l]) {
                var s = "function" == typeof require && require;
                if (!t && s) return s(l, !0);
                if (i) return i(l, !0);
                var c = new Error("Cannot find module '" + l + "'");
                throw c.code = "MODULE_NOT_FOUND", c
            }
            var d = r[l] = { exports: {} };
            o[l][0].call(d.exports,
                function (e) {
                    var r = o[l][1][e];
                    return a(r ? r : e)
                },
                d,
                d.exports,
                e,
                o,
                r,
                n)
        }
        return r[l].exports
    }

    for (var i = "function" == typeof require && require, l = 0; l < n.length; l++) a(n[l]);
    return a
} ({
    1: [
        function (e, o, r) {
            !function () {
                "use strict";
                $(document).ready(function () {
                    $(window).bind("scroll",
                        function () {
                            var e = 150;
                            $(window).scrollTop() > e
                                ? $(".header").addClass("header--fixed")
                                : $(".header").removeClass("header--fixed")
                        }), $(".mobile-nav-handler").click(function (e) {
                            $(".mobile-nav").toggleClass("mobile-nav--open"), $(".header").toggleClass("header--hide"),
                                $("body").toggleClass("no-scroll"), $("#toggle-nav").toggleClass("active")
                        }), $(".nav-link").click(function (e) {
                            $(".mobile-nav").removeClass("mobile-nav--open"), $(".header").removeClass("header--hide"),
                                $("body").removeClass("no-scroll"), $("#toggle-nav").removeClass("active")
                        })
                })
            }()
        }, {}
    ]
},
    {},
    [1]);
