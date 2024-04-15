function loadResults(data) {

  


    $('#searchResults').empty();

   
    for (var i = 0; i < data.length; i++) {

        var imageUrl = data[i].imageUrl ? '/uploads/' + data[i].imageUrl : '/uploads/default.jpg';
        var listingId = parseInt(data[i].listingId);
        var price = parseInt(data[i].price);


        var searchResultsHtml = '<div class="container mb-5 mt-5" style="padding: 0; width: 60%">';
        searchResultsHtml += '<div class="row border p-0 d-flex listingContainer">';
        searchResultsHtml += '<img class="p-0 listingImage" src="' + imageUrl + '" class="img-fluid" alt="Travel Listing Image">';
        searchResultsHtml += '<div class="namePrice">';
        searchResultsHtml += '<div class="name">';
        searchResultsHtml += '<a href="/Listing/Details/Details/' + listingId + '">' + data[i].name + '</a>';
        searchResultsHtml += '</div>';
        searchResultsHtml += '<div class="price">';
        searchResultsHtml += '$' + price;
        searchResultsHtml += '</div>';
        searchResultsHtml += '</div>';
        searchResultsHtml += '</div>';
        searchResultsHtml += '</div>';




        $('#searchResults').append(searchResultsHtml);


    }
    
}




$(document).ready(function () {


  

    $('#searchForm').submit(function (e) {
            
        e.preventDefault();

        var formData = {

            SearchTerm: $('#searchTerm').val(),
            Available: $('#available').is(':checked'),
            Unavailable: $('#unavailable').is(':checked'),
            FlightDate: $('#flightDate').is(':checked'),
            Location: $('#location').is(':checked'),
            Destination: $('#destination').is(':checked'),
            Model: $('#model').is(':checked'),
            Manufacturer: $('#manufacturer').is(':checked'),
            MinPrice: $('#minPrice').is(':checked'),
            MaxPrice: $('#maxPrice').is(':checked'),
        };

        
   
       
        $.ajax({

            url: '/Listing/SearchResults',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {

              

                if (response.success) {

                 


                    loadResults(response.data);
                } else {

                  

                    alert(response.message);
                }



            },
            error: function (xhr, status, error) {
              
                alert("Error " + error);

            }
            });
            });

        });
