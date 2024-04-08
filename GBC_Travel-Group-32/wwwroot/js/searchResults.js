

function loadResults(data) {

    $('#searchResults').empty();

  


 
    data.forEach(function (listing) {

     

        var imageUrl = listing[2] ? '/uploads/' + listing[2] : '/uploads/default.jpg';
        var listingId = parseInt(listing[0]);
        var price = parseInt(listing[3]);


        var searchResultsHtml = '<div class="container mb-5 mt-5" style="padding: 0; width: 60%">';
        searchResultsHtml += '<div class="row border p-0 d-flex listingContainer">';
        searchResultsHtml += '<img class="p-0 listingImage" src="' + imageUrl + '" class="img-fluid" alt="Travel Listing Image">';
        searchResultsHtml += '<div class="namePrice">';
        searchResultsHtml += '<div class="name">';
        searchResultsHtml += '<a href="Details/' + listingId + '">' + listing[1] + '</a>';
        searchResultsHtml += '</div>';
        searchResultsHtml += '<div class="price">';
        searchResultsHtml += '$' + price;
        searchResultsHtml += '</div>';
        searchResultsHtml += '</div>';
        searchResultsHtml += '</div>';
        searchResultsHtml += '</div>';




        $('#searchResults').append(searchResultsHtml);

    })


}



$(document).ready(function () {


  

        $('#searchForm').submit(function (e) {
            e.preventDefault();

            $.ajax({

                url: '/Listing/SearchResults',
                method: 'GET',
                data: $(this).serialize(),
                success: function (response) {

                    if (response.success) {
                     
                        $('#searchResults').val('');
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