function loadReviews(listingId) {

    $.ajax({

        url: '/ListingReview/GetReviews?listingId=' + listingId,
        method: 'GET',

        success: function (data) {

            var reviewHtml = '';
            for (var i = 0; i < data.length; i++) {
                reviewHtml += '<div class="reviewBox">';
                reviewHtml += '<p class="review">' + data[i].content + '</p>';
                reviewHtml += '<span>Posted on ' + new Date(data[i].datePosted).toLocaleString() + '</span>';
                reviewHtml += '<hr>'
                reviewHtml += '</div>'
            }
            $('#reviewList').html(reviewHtml);

        }


    })



}


$(document).ready(function () {


    var listingId = $('#listingReviews input[name="ListingId"]').val();

    var userId = $('#listingReviews input[name="UserId"]').val();

    loadReviews(listingId);

    $('#addReviewForm').submit(function (e) {


        e.preventDefault();
        var formData = {
            ListingId: listingId,
            UserId: userId,
            Content: $('#listingReviews textarea[name="Content"]').val()
        };

        $.ajax({

            url: '/ListingReview/AddReview',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),

            success: function (response) {

                if (response.success) {
                    $('#listingReviews input[name="Content"]').val('');
                    loadReviews(listingId);
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