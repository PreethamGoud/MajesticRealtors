/*!
* Start Bootstrap - Agency v7.0.12 (https://startbootstrap.com/theme/agency)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-agency/blob/master/LICENSE)
*/
//
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Navbar shrink function
    var navbarShrink = function () {
        const navbarCollapsible = document.body.querySelector('#mainNav');
        if (!navbarCollapsible) {
            return;
        }
        if (window.scrollY === 0) {
            navbarCollapsible.classList.remove('navbar-shrink')
        } else {
            navbarCollapsible.classList.add('navbar-shrink')
        }

    };

    // Shrink the navbar 
    navbarShrink();

    // Shrink the navbar when page is scrolled
    document.addEventListener('scroll', navbarShrink);

    //  Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            rootMargin: '0px 0px -40%',
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

});

// Function to initialize the map
var map;
var marker;
var currentAddress; // Track the current address

// Function to initialize the map
function initMap() {
    // Initialize the map with default options
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 41.8781, lng: -87.6298 }, // Default to Chicago
        zoom: 12
    });

    // Event listener for table row clicks
    $('#table-id tbody tr').on('click', function () {
        // Get the address from the data attribute
        var address = $(this).data('address');

        // Use the address to geocode and update the map
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                // Clear existing marker
                if (marker) {
                    marker.setMap(null);
                }

                // Set center and add new marker
                map.setCenter(results[0].geometry.location);
                marker = new google.maps.Marker({
                    map: map,
                    position: results[0].geometry.location,
                    title: address
                });

                // Show address text on the map
                var infowindow = new google.maps.InfoWindow({
                    content: '<div><strong>' + address + '</strong></div>'
                });
                infowindow.open(map, marker);

                // Update current address
                currentAddress = address;
            } else {
                alert('Geocode was not successful for the following reason: ' + status);
            }
        });
    });

    // Event listener for "Navigate" button click
    $('#navigate-btn').on('click', function () {
        // Check if currentAddress is set
        if (currentAddress) {
            // Open the default mapping application with the specified address
            window.open('https://www.google.com/maps/search/?api=1&query=' + encodeURIComponent(currentAddress), '_blank');
        } else {
            alert('Please select a location from the table first.');
        }
    });
}