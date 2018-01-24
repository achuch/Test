console.log("ready!");
var map;
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 50.059306, lng: 19.928298 },
        zoom: 12
    });
    var marker = new google.maps.Marker({
        position: { lat: 50.069678, lng: 19.909633 },
        map: map,
        title: "Nawojki!"
    });
}