function show_loading() {
    jQuery(".loading-box").fadeIn();
}

function hide_loading() {
    jQuery(".loading-box").fadeOut();
}

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function product_category_left_side_expand(id) {
    var item = jQuery("#ProductCategory_LeftSideBar a[cat_id=" + id + "]");

    //    var leftside_expander = item.parent().find(">.grower");
    //    if (leftside_expander.length > 0) {
    //        leftside_expander.trigger("click");
    //    }

    // go back to parent
    item = item.parent().parent().parent();
    var tagName = item.prop("tagName");
    while (tagName == "LI") {
        // expand this one
        leftside_expander = item.find(">.grower");
        if (leftside_expander.length > 0) {
            leftside_expander.trigger("click");
        }
        // then keep going moving
        item = item.parent().parent().parent();
        tagName = item.prop("tagName");
    }

}

(function (jQuery) {

    jQuery.fn.BreakingNews = function (settings) {
        var defaults = {
            background: '#FFF',
            title: 'NEWS',
            titlecolor: '#FFF',
            titlebgcolor: '#5aa628',
            linkcolor: '#333',
            linkhovercolor: '#5aa628',
            fonttextsize: 16,
            isbold: false,
            border: 'none',
            width: '100%',
            autoplay: true,
            timer: 3000,
            modulid: 'brekingnews',
            effect: 'fade'	//or slide	
        };
        var settings = $.extend(defaults, settings);

        return this.each(function () {
            settings.modulid = "#" + $(this).attr("id");
            var timername = settings.modulid;
            var activenewsid = 1;

            if (settings.isbold == true)
                fontw = 'bold';
            else
                fontw = 'normal';

            if (settings.effect == 'slide')
                $(settings.modulid + ' ul li').css({ 'display': 'block' });
            else
                $(settings.modulid + ' ul li').css({ 'display': 'none' });

            $(settings.modulid + ' .bn-title').html(settings.title);
            $(settings.modulid).css({ 'width': settings.width, 'background': settings.background, 'border': settings.border, 'font-size': settings.fonttextsize });
            $(settings.modulid + ' ul').css({ 'left': $(settings.modulid + ' .bn-title').width() + 40 });
            $(settings.modulid + ' .bn-title').css({ 'background': settings.titlebgcolor, 'color': settings.titlecolor, 'font-weight': fontw });
            $(settings.modulid + ' ul li a').css({ 'color': settings.linkcolor, 'font-weight': fontw, 'height': parseInt(settings.fonttextsize) + 6 });
            $(settings.modulid + ' ul li').eq(parseInt(activenewsid - 1)).css({ 'display': 'block' });

            // Links hover events ......
            $(settings.modulid + ' ul li a').hover(function () {
                $(this).css({ 'color': settings.linkhovercolor });
            },
					function () {
					    $(this).css({ 'color': settings.linkcolor });
					}
				);


            // Arrows Click Events ......
            $(settings.modulid + ' .bn-arrows span').click(function (e) {
                if ($(this).attr('class') == "bn-arrows-left")
                    BnAutoPlay('prev');
                else
                    BnAutoPlay('next');
            });

            // Timer events ...............
            if (settings.autoplay == true) {
                timername = setInterval(function () { BnAutoPlay('next') }, settings.timer);
                $(settings.modulid).hover(function () {
                    clearInterval(timername);
                },
						function () {
						    timername = setInterval(function () { BnAutoPlay('next') }, settings.timer);
						}
					);
            }
            else {
                clearInterval(timername);
            }

            //timer and click events function ...........
            function BnAutoPlay(pos) {
                if (pos == "next") {
                    if ($(settings.modulid + ' ul li').length > activenewsid)
                        activenewsid++;
                    else
                        activenewsid = 1;
                }
                else {
                    if (activenewsid - 2 == -1)
                        activenewsid = $(settings.modulid + ' ul li').length;
                    else
                        activenewsid = activenewsid - 1;
                }

                if (settings.effect == 'fade') {
                    $(settings.modulid + ' ul li').css({ 'display': 'none' });
                    $(settings.modulid + ' ul li').eq(parseInt(activenewsid - 1)).fadeIn();
                }
                else {
                    $(settings.modulid + ' ul').animate({ 'marginTop': -($(settings.modulid + ' ul li').height() + 20) * (activenewsid - 1) });
                }
            }

            // links size calgulating function ...........
            $(window).resize(function (e) {
                if ($(settings.modulid).width() < 360) {
                    $(settings.modulid + ' .bn-title').html('&nbsp;');
                    $(settings.modulid + ' .bn-title').css({ 'width': '4px', 'padding': '10px 0px' });
                    $(settings.modulid + ' ul').css({ 'left': 4 });
                } else {
                    $(settings.modulid + ' .bn-title').html(settings.title);
                    $(settings.modulid + ' .bn-title').css({ 'width': 'auto', 'padding': '10px 20px' });
                    $(settings.modulid + ' ul').css({ 'left': $(settings.modulid + ' .bn-title').width() + 40 });
                }
            });
        });

    };

})(jQuery);


/**
* jQuery gMap v3
*
* @url         http://www.smashinglabs.pl/gmap
* @author      Sebastian Poreba <sebastian.poreba@gmail.com>
* @version     3.3.3
* @date        27.12.2012
*/
(function (j) {
    var p = function () { this.markers = []; this.mainMarker = !1; this.icon = "http://www.google.com/mapfiles/marker.png" }; p.prototype.dist = function (a) { return Math.sqrt(Math.pow(this.markers[0].latitude - a.latitude, 2) + Math.pow(this.markers[0].longitude - a.longitude, 2)) }; p.prototype.setIcon = function (a) { this.icon = a }; p.prototype.addMarker = function (a) { this.markers[this.markers.length] = a }; p.prototype.getMarker = function () {
        if (this.mainmarker) return this.mainmarker; var a, b; 1 < this.markers.length ? (a = new e.MarkerImage("http://thydzik.com/thydzikGoogleMap/markerlink.php?text=" +
    this.markers.length + "&color=EF9D3F"), b = "cluster of " + this.markers.length + " markers") : (a = new e.MarkerImage(this.icon), b = this.markers[0].title); return this.mainmarker = new e.Marker({ position: new e.LatLng(this.markers[0].latitude, this.markers[0].longitude), icon: a, title: b, map: null })
    }; var e = google.maps, q = new e.Geocoder, l = 0, r = 0, f = {}, f = { init: function (a) {
        var b, c = j.extend({}, j.fn.gMap.defaults, a); for (b in j.fn.gMap.defaults.icon) c.icon[b] || (c.icon[b] = j.fn.gMap.defaults.icon[b]); return this.each(function () {
            var a =
    j(this), b = f._getMapCenter.apply(a, [c]); "fit" == c.zoom && (c.zoomFit = !0, c.zoom = f._autoZoom.apply(a, [c])); var g = { zoom: c.zoom, center: b, mapTypeControl: c.mapTypeControl, mapTypeControlOptions: {}, zoomControl: c.zoomControl, zoomControlOptions: {}, panControl: c.panControl, panControlOptions: {}, scaleControl: c.scaleControl, scaleControlOptions: {}, streetViewControl: c.streetViewControl, streetViewControlOptions: {}, mapTypeId: c.maptype, scrollwheel: c.scrollwheel, maxZoom: c.maxZoom, minZoom: c.minZoom }; c.controlsPositions.mapType &&
(g.mapTypeControlOptions.position = c.controlsPositions.mapType); c.controlsPositions.zoom && (g.zoomControlOptions.position = c.controlsPositions.zoom); c.controlsPositions.pan && (g.panControlOptions.position = c.controlsPositions.pan); c.controlsPositions.scale && (g.scaleControlOptions.position = c.controlsPositions.scale); c.controlsPositions.streetView && (g.streetViewControlOptions.position = c.controlsPositions.streetView); c.styles && (g.styles = c.styles); g.mapTypeControlOptions.style = c.controlsStyle.mapType; g.zoomControlOptions.style =
    c.controlsStyle.zoom; g = new e.Map(this, g); c.log && console.log("map center is:"); c.log && console.log(b); a.data("$gmap", g); a.data("gmap", { opts: c, gmap: g, markers: [], markerKeys: {}, infoWindow: null, clusters: [] }); if (0 !== c.controls.length) for (b = 0; b < c.controls.length; b += 1) g.controls[c.controls[b].pos].push(c.controls[b].div); c.clustering.enabled ? (b = a.data("gmap"), b.markers = c.markers, f._renderCluster.apply(a, []), e.event.addListener(g, "bounds_changed", function () { f._renderCluster.apply(a, []) })) : 0 !== c.markers.length &&
    f.addMarkers.apply(a, [c.markers]); f._onComplete.apply(a, [])
        })
    }, _delayedMode: !1, _onComplete: function () { var a = this.data("gmap"), b = this; if (0 !== l) window.setTimeout(function () { f._onComplete.apply(b, []) }, 100); else { if (f._delayedMode) { var c = f._getMapCenter.apply(this, [a.opts, !0]); f._setMapCenter.apply(this, [c]); a.opts.zoomFit && (c = f._autoZoom.apply(this, [a.opts, !0]), a.gmap.setZoom(c)) } a.opts.onComplete() } }, _setMapCenter: function (a) {
        var b = this.data("gmap"); b && b.opts.log && console.log("delayed setMapCenter called");
        if (b && void 0 !== b.gmap && 0 == l) b.gmap.setCenter(a); else { var c = this; window.setTimeout(function () { f._setMapCenter.apply(c, [a]) }, 100) }
    }, _boundaries: null, _getBoundaries: function (a) {
        var b = a.markers, c, h = 1E3, d = -1E3, g = 1E3, e = -1E3; if (b) {
            for (c = 0; c < b.length; c += 1) b[c].latitude && b[c].longitude && (h > b[c].latitude && (h = b[c].latitude), d < b[c].longitude && (d = b[c].longitude), g > b[c].longitude && (g = b[c].longitude), e < b[c].latitude && (e = b[c].latitude), a.log && console.log(b[c].latitude, b[c].longitude, h, d, g, e)); f._boundaries = { N: h,
                E: d, W: g, S: e
            }
        } -1E3 == h && (f._boundaries = { N: 0, E: 0, W: 0, S: 0 }); return f._boundaries
    }, _getBoundariesFromMarkers: function () {
        var a = this.data("gmap").markers, b, c = 1E3, h = -1E3, d = 1E3, g = -1E3; if (a) { for (b = 0; b < a.length; b += 1) c > a[b].getPosition().lat() && (c = a[b].getPosition().lat()), h < a[b].getPosition().lng() && (h = a[b].getPosition().lng()), d > a[b].getPosition().lng() && (d = a[b].getPosition().lng()), g < a[b].getPosition().lat() && (g = a[b].getPosition().lat()); f._boundaries = { N: c, E: h, W: d, S: g} } -1E3 == c && (f._boundaries = { N: 0, E: 0, W: 0,
            S: 0
        }); return f._boundaries
    }, _getMapCenter: function (a, b) {
        var c, h = this, d, g; if (a.markers.length && ("fit" == a.latitude || "fit" == a.longitude)) return d = b ? f._getBoundariesFromMarkers.apply(this) : f._getBoundaries(a), c = new e.LatLng((d.N + d.S) / 2, (d.E + d.W) / 2), a.log && console.log(b, d, c), c; if (a.latitude && a.longitude) return c = new e.LatLng(a.latitude, a.longitude); c = new e.LatLng(0, 0); if (a.address) return q.geocode({ address: a.address }, function (b, c) {
            c === google.maps.GeocoderStatus.OK ? f._setMapCenter.apply(h, [b[0].geometry.location]) :
    a.log && console.log("Geocode was not successful for the following reason: " + c)
        }), c; if (0 < a.markers.length) {
            g = null; for (d = 0; d < a.markers.length; d += 1) if (a.markers[d].setCenter) { g = a.markers[d]; break } if (null === g) for (d = 0; d < a.markers.length; d += 1) { if (a.markers[d].latitude && a.markers[d].longitude) { g = a.markers[d]; break } a.markers[d].address && (g = a.markers[d]) } if (null === g) return c; if (g.latitude && g.longitude) return new e.LatLng(g.latitude, g.longitude); g.address && q.geocode({ address: g.address }, function (b, c) {
                c === google.maps.GeocoderStatus.OK ?
    f._setMapCenter.apply(h, [b[0].geometry.location]) : a.log && console.log("Geocode was not successful for the following reason: " + c)
            })
        } return c
    }, _renderCluster: function () {
        var a = this.data("gmap"), b = a.markers, c = a.clusters, h = a.opts, d; for (d = 0; d < c.length; d += 1) c[d].getMarker().setMap(null); c.length = 0; if (d = a.gmap.getBounds()) {
            var g = d.getNorthEast(), e = d.getSouthWest(), k = [], m = (g.lat() - e.lat()) * h.clustering.clusterSize / 100; for (d = 0; d < b.length; d += 1) b[d].latitude < g.lat() && (b[d].latitude > e.lat() && b[d].longitude < g.lng() &&
    b[d].longitude > e.lng()) && (k[k.length] = b[d]); h.log && console.log("number of markers " + k.length + "/" + b.length); h.log && console.log("cluster radius: " + m); for (d = 0; d < k.length; d += 1) { g = -1; for (b = 0; b < c.length && !(e = c[b].dist(k[d]), e < m && (g = b, h.clustering.fastClustering)); b += 1); -1 === g ? (b = new p, b.addMarker(k[d]), c[c.length] = b) : c[g].addMarker(k[d]) } h.log && console.log("Total clusters in viewport: " + c.length); for (b = 0; b < c.length; b += 1) c[b].getMarker().setMap(a.gmap)
        } else {
            var j = this; window.setTimeout(function () { f._renderCluster.apply(j) },
    1E3)
        }
    }, _processMarker: function (a, b, c, h) {
        var d = this.data("gmap"), g = d.gmap, f = d.opts, k; void 0 === h && (h = new e.LatLng(a.latitude, a.longitude)); if (!b) { var j = { image: f.icon.image, iconSize: new e.Size(f.icon.iconsize[0], f.icon.iconsize[1]), iconAnchor: new e.Point(f.icon.iconanchor[0], f.icon.iconanchor[1]), infoWindowAnchor: new e.Size(f.icon.infowindowanchor[0], f.icon.infowindowanchor[1]) }; b = new e.MarkerImage(j.image, j.iconSize, null, j.iconAnchor) } c || (new e.Size(f.icon.shadowsize[0], f.icon.shadowsize[1]), j && j.iconAnchor ||
    new e.Point(f.icon.iconanchor[0], f.icon.iconanchor[1])); b = { position: h, icon: b, title: a.title, map: null, draggable: !0 === a.draggable ? !0 : !1 }; f.clustering.enabled || (b.map = g); k = new e.Marker(b); k.setShadow(c); d.markers.push(k); a.key && (d.markerKeys[a.key] = k); var n; a.html && (c = { content: "string" === typeof a.html ? f.html_prepend + a.html + f.html_append : a.html, pixelOffset: a.infoWindowAnchor }, f.log && console.log("setup popup with data"), f.log && console.log(c), n = new e.InfoWindow(c), e.event.addListener(k, "click", function () {
        f.log &&
console.log("opening popup " + a.html); f.singleInfoWindow && d.infoWindow && d.infoWindow.close(); n.open(g, k); d.infoWindow = n
    })); a.html && a.popup && (f.log && console.log("opening popup " + a.html), n.open(g, k), d.infoWindow = n); a.onDragEnd && e.event.addListener(k, "dragend", function (b) { f.log && console.log("drag end"); a.onDragEnd(b) })
    }, _geocodeMarker: function (a, b, c) {
        var h = this; q.geocode({ address: a.address }, function (d, g) {
            g === e.GeocoderStatus.OK ? (l -= 1, h.data("gmap").opts.log && console.log("Geocode was successful with point: ",
    d[0].geometry.location), f._processMarker.apply(h, [a, b, c, d[0].geometry.location])) : (g === e.GeocoderStatus.OVER_QUERY_LIMIT && (!h.data("gmap").opts.noAlerts && 0 === r && alert("Error: too many geocoded addresses! Switching to 1 marker/s mode."), r += 1E3, window.setTimeout(function () { f._geocodeMarker.apply(h, [a, b, c]) }, r)), h.data("gmap").opts.log && console.log("Geocode was not successful for the following reason: " + g))
        })
    }, _autoZoom: function (a, b) {
        var c = j(this).data("gmap"), e = j.extend({}, c ? c.opts : {}, a), d, g, c = 39135.758482;
        e.log && console.log("autozooming map"); d = b ? f._getBoundariesFromMarkers.apply(this) : f._getBoundaries(e); e = 111E3 * (d.E - d.W) / this.width(); g = 111E3 * (d.S - d.N) / this.height(); for (d = 2; 20 > d && !(e > c || g > c); d += 1) c /= 2; return d - 2
    }, addMarkers: function (a) { var b = this.data("gmap").opts; if (0 !== a.length) { b.log && console.log("adding " + a.length + " markers"); for (b = 0; b < a.length; b += 1) f.addMarker.apply(this, [a[b]]) } }, addMarker: function (a) {
        var b = this.data("gmap").opts; b.log && console.log("putting marker at " + a.latitude + ", " + a.longitude +
    " with address " + a.address + " and html " + a.html); var c = b.icon.image, h = new e.Size(b.icon.iconsize[0], b.icon.iconsize[1]), d = new e.Point(b.icon.iconanchor[0], b.icon.iconanchor[1]), g = new e.Size(b.icon.infowindowanchor[0], b.icon.infowindowanchor[1]), j = b.icon.shadow, k = new e.Size(b.icon.shadowsize[0], b.icon.shadowsize[1]), m = new e.Point(b.icon.shadowanchor[0], b.icon.shadowanchor[1]); a.infoWindowAnchor = g; a.icon && (a.icon.image && (c = a.icon.image), a.icon.iconsize && (h = new e.Size(a.icon.iconsize[0], a.icon.iconsize[1])),
    a.icon.iconanchor && (d = new e.Point(a.icon.iconanchor[0], a.icon.iconanchor[1])), a.icon.infowindowanchor && new e.Size(a.icon.infowindowanchor[0], a.icon.infowindowanchor[1]), a.icon.shadow && (j = a.icon.shadow), a.icon.shadowsize && (k = new e.Size(a.icon.shadowsize[0], a.icon.shadowsize[1])), a.icon.shadowanchor && (m = new e.Point(a.icon.shadowanchor[0], a.icon.shadowanchor[1]))); c = new e.MarkerImage(c, h, null, d); j = new e.MarkerImage(j, k, null, m); a.address ? ("_address" === a.html && (a.html = a.address), "_address" == a.title && (a.title =
    a.address), b.log && console.log("geocoding marker: " + a.address), l += 1, f._delayedMode = !0, f._geocodeMarker.apply(this, [a, c, j])) : ("_latlng" === a.html && (a.html = a.latitude + ", " + a.longitude), "_latlng" == a.title && (a.title = a.latitude + ", " + a.longitude), b = new e.LatLng(a.latitude, a.longitude), f._processMarker.apply(this, [a, c, j, b]))
    }, removeAllMarkers: function () { var a = this.data("gmap").markers, b; for (b = 0; b < a.length; b += 1) a[b].setMap(null), delete a[b]; a.length = 0 }, getMarker: function (a) { return this.data("gmap").markerKeys[a] },
        fixAfterResize: function (a) { var b = this.data("gmap"); e.event.trigger(b.gmap, "resize"); a && b.gmap.panTo(new google.maps.LatLng(0, 0)); b.gmap.panTo(this.gMap("_getMapCenter", b.opts)) }, setZoom: function (a, b, c) { var e = this.data("gmap").gmap; "fit" === a && (a = f._autoZoom.apply(this, [b, c])); e.setZoom(parseInt(a)) }, changeSettings: function (a) {
            var b = this.data("gmap"), c = [], e; for (e = 0; e < b.markers.length; e += 1) c[e] = { latitude: b.markers[e].getPosition().lat(), longitude: b.markers[e].getPosition().lng() }; a.markers = c; a.zoom &&
  f.setZoom.apply(this, [a.zoom, a]); (a.latitude || a.longitude) && b.gmap.panTo(f._getMapCenter.apply(this, [a]))
        }, mapclick: function (a) { google.maps.event.addListener(this.data("gmap").gmap, "click", function (b) { a(b.latLng) }) }, geocode: function (a, b, c) { q.geocode({ address: a }, function (a, d) { d === e.GeocoderStatus.OK ? b(a[0].geometry.location) : c && c(a, d) }) }, getRoute: function (a) {
            var b = this.data("gmap"), c = b.gmap, f = new e.DirectionsRenderer, d = new e.DirectionsService, g = { BYCAR: e.DirectionsTravelMode.DRIVING, BYBICYCLE: e.DirectionsTravelMode.BICYCLING,
                BYFOOT: e.DirectionsTravelMode.WALKING
            }, l = { MILES: e.DirectionsUnitSystem.IMPERIAL, KM: e.DirectionsUnitSystem.METRIC }, k = null, m = null, n = null; void 0 !== a.routeDisplay ? k = a.routeDisplay instanceof jQuery ? a.routeDisplay[0] : "string" == typeof a.routeDisplay ? j(a.routeDisplay)[0] : null : null !== b.opts.routeFinder.routeDisplay && (k = b.opts.routeFinder.routeDisplay instanceof jQuery ? b.opts.routeFinder.routeDisplay[0] : "string" == typeof b.opts.routeFinder.routeDisplay ? j(b.opts.routeFinder.routeDisplay)[0] : null); f.setMap(c);
            null !== k && f.setPanel(k); m = void 0 !== g[b.opts.routeFinder.travelMode] ? g[b.opts.routeFinder.travelMode] : g.BYCAR; n = void 0 !== l[b.opts.routeFinder.travelUnit] ? l[b.opts.routeFinder.travelUnit] : l.KM; d.route({ origin: a.from, destination: a.to, travelMode: m, unitSystem: n }, function (a, c) { c == e.DirectionsStatus.OK ? f.setDirections(a) : null !== k && j(k).html(b.opts.routeFinder.routeErrors[c]) }); return this
        }
    }; j.fn.gMap = function (a) {
        if (f[a]) return f[a].apply(this, Array.prototype.slice.call(arguments, 1)); if ("object" === typeof a ||
    !a) return f.init.apply(this, arguments); j.error("Method " + a + " does not exist on jQuery.gmap")
    }; j.fn.gMap.defaults = { log: !1, address: "", latitude: null, longitude: null, zoom: 3, maxZoom: null, minZoom: null, markers: [], controls: {}, scrollwheel: !0, maptype: google.maps.MapTypeId.ROADMAP, mapTypeControl: !0, zoomControl: !0, panControl: !1, scaleControl: !1, streetViewControl: !0, controlsPositions: { mapType: null, zoom: null, pan: null, scale: null, streetView: null }, controlsStyle: { mapType: google.maps.MapTypeControlStyle.DEFAULT, zoom: google.maps.ZoomControlStyle.DEFAULT },
        singleInfoWindow: !0, html_prepend: '<div class="gmap_marker">', html_append: "</div>", icon: { image: "http://www.google.com/mapfiles/marker.png", iconsize: [20, 34], iconanchor: [9, 34], infowindowanchor: [9, 2], shadow: "http://www.google.com/mapfiles/shadow50.png", shadowsize: [37, 34], shadowanchor: [9, 34] }, onComplete: function () { }, routeFinder: { travelMode: "BYCAR", travelUnit: "KM", routeDisplay: null, routeErrors: { INVALID_REQUEST: "The provided request is invalid.", NOT_FOUND: "One or more of the given addresses could not be found.",
            OVER_QUERY_LIMIT: "A temporary error occured. Please try again in a few minutes.", REQUEST_DENIED: "An error occured. Please contact us.", UNKNOWN_ERROR: "An unknown error occured. Please try again.", ZERO_RESULTS: "No route could be found within the given addresses."
        }
        }, clustering: { enabled: !1, fastClustering: !1, clusterCount: 10, clusterSize: 40 }
    }
})(jQuery);

jQuery.browser = {};
(function () {
    jQuery.browser.msie = false;
    jQuery.browser.version = 0;
    if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
        jQuery.browser.msie = true;
        jQuery.browser.version = RegExp.$1;
    }
})();


/*jQuery(document).ready(function(){
jQuery(".select2").select2();

// for product categories  on left side
if (!$('ul.tree.dhtml').hasClass('dynamized')) 
{
$('ul.tree.dhtml ul').prev().before("<span class='grower OPEN'> </span>");
$('ul.tree.dhtml ul li:last-child, ul.tree.dhtml li:last-child').addClass('last');
$('ul.tree.dhtml span.grower.OPEN').addClass('CLOSE').removeClass('OPEN').parent().find('ul:first').hide();
$('ul.tree.dhtml').show();
$('ul.tree.dhtml .selected').parents().each(function() {
if ($(this).is('ul'))
toggleBranch($(this).prev().prev(), true);
});
toggleBranch($('ul.tree.dhtml .selected').prev(), true);
$('ul.tree.dhtml span.grower').click(function() {
toggleBranch($(this));
});
$('ul.tree.dhtml').addClass('dynamized');
$('ul.tree.dhtml').removeClass('dhtml');
}

// for product detail zoom
$('a.szoom').jqzoom({
zoomType:'innerzoom',
preloadImages: true
}
);
   
// product detail carousel
$("#thumbs_list_frame").carouFredSel({
items               : 4,
infinite: true,     
scroll : {
items           : 1,
effect          : "easeOutBounce",
duration        : 500,
pauseOnHover    : true
},
prev: {
button: "#view_scroll_left"
},
next: {
button: "#view_scroll_right"
}
});

// for product detail - click to update new images
jQuery("#thumbs_list_frame li a").click(function(){
var image=jQuery(this).attr("href");
//jQuery("#bigpic").attr("src",image);
//jQuery(".zoomWrapperImage > img").attr("src",image);
jQuery("#view_full_size").html("<a href='"+image+"' class='szoom'><img itemprop='image' src='"+image+"' id='bigpic' /></a><a href='#' class='span_link'></a>");
$('a.szoom').jqzoom({
zoomType:'innerzoom',
preloadImages: true
});
return false;
});
});*/