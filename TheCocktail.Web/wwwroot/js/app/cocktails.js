$(function () {
    $('#notiFavorites .goFavorites').hide();

    $('#btnAddFavorite').on('click', function (event) {
        event.preventDefault();

        var drinkId = $(this).data('drinkid');
        var favoriteId = $(this).data('favoriteid');
        var drinkname = $(this).data('drinkname');
        var drinkimage = $(this).data('drinkimage');

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/FavoriteCocktails/AddRemoveFavorite",
            data: {
                DrinkId: drinkId,
                FavoriteId: favoriteId,
                DrinkImage: drinkimage,
                DrinkName: drinkname
            },
            success: function (result) {
                if (result.success) {

                    $('#btnAddFavorite')
                        .removeAttr('data-favoriteid')
                        .removeAttr('title');

                    if (result.isAdded) {
                        $('#icon-favorite')
                            .removeClass('bi-heart')
                            .addClass('bi-heart-fill');

                        $('#notiFavorites .notiFavoritesTitle')
                            .text('Guardado en favoritos');
                        $('#notiFavorites .goFavorites').show();
                        $('#btnAddFavorite').data('favoriteid', result.id);
                        $('#btnAddFavorite').attr('title', 'Quitar de favoritos');
                    }
                    else {
                        $('#icon-favorite')
                            .removeClass('bi-heart-fill')
                            .addClass('bi-heart');

                        $('#notiFavorites .notiFavoritesTitle')
                            .text('Eliminado de favoritos');
                        $('#notiFavorites .goFavorites').hide();
                        $('#btnAddFavorite').data('favoriteid', 0);
                        $('#btnAddFavorite').attr('title', 'Agregar a favoritos');
                    }
                } else {
                    $('#notiFavorites .notiFavoritesTitle')
                        .text(result.message);
                    $('#notiFavorites .goFavorites').hide();
                }
            },
            error: function () {
                $('#notiFavorites .notiFavoritesTitle')
                    .text('Ocurrion un error y no se pudo agregar a favoritos');
                $('#notiFavorites .goFavorites').hide();
            },
            complete: function () {
                $('#notiFavorites').toast('show');
            }
        })
    });
});