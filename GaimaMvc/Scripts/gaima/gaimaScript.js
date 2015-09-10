$(document).ready(function () {
        $('#slider-strategy').slider({
            orientation: 'vertical',
            range: 'min',
            value: 0,
            min: 0,
            max: 5,
            step: 1,
            slide: function (event, ui) {
                $('#amount-strategy').val(ui.value);
            }
        });
        $('#slider-mechanics').slider({
            orientation: 'vertical',
            range: 'min',
            value: 0,
            min: 0,
            max: 5,
            step: 1,
            slide: function (event, ui) {
                $('#amount-mechanics').val(ui.value);
            }
        });
        $('#slider-ease').slider({
            orientation: 'vertical',
            range: 'min',
            value: 0,
            min: 0,
            max: 5,
            step: 1,
            slide: function (event, ui) {
                $('#amount-ease').val(ui.value);
            }
        });
        $('#slider-atmosphere').slider({
            orientation: 'vertical',
            range: 'min',
            value: 0,
            min: 0,
            max: 5,
            step: 1,
            slide: function (event, ui) {
                $('#amount-atmosphere').val(ui.value);
            }
        });
        $('#slider-longevity').slider({
            orientation: 'vertical',
            range: 'min',
            value: 0,
            min: 0,
            max: 5,
            step: 1,
            slide: function (event, ui) {
                $('#amount-longevity').val(ui.value);
            }
        });

        $('#amount-strategy').val($('#slider-strategy').slider('value'));
        $('#amount-mechanics').val($('#slider-mechanics').slider('value'));
        $('#amount-ease').val($('#slider-ease').slider('value'));
        $('#amount-atmosphere').val($('#slider-atmosphere').slider('value'));
        $('#amount-longevity').val($('#slider-longevity').slider('value'));


    $('#SelectPlatformID').change(function () {
        $('#parameterContainer').append('<div class="parameter btn btn-sm" id="platform_' + $('#SelectPlatformID').val() + '">' + $('#SelectPlatformID option:selected').text() + '<span class="glyphicon glyphicon-remove"></div>')
        $('#SelectPlatformID').val("");
    });
    $('#SelectGenreID').change(function () {
        $('#parameterContainer').append('<div class="parameter btn btn-sm" id="genre_' + $('#SelectGenreID').val() + '">' + $('#SelectGenreID option:selected').text() + '<span class="glyphicon glyphicon-remove"></div>')
        $('#SelectGenreID').val("");
    });
    $('#SelectGraphicStyleID').change(function () {
        $('#parameterContainer').append('<div class="parameter btn btn-sm" id="graphicStyle_' + $('#SelectGraphicStyleID').val() + '">' + $('#SelectGraphicStyleID option:selected').text() + '<span class="glyphicon glyphicon-remove"></div>')
        $('#SelectGraphicStyleID').val("");
    });
    $('#SelectThemeID').change(function () {
        $('#parameterContainer').append('<div class="parameter btn btn-sm" id="theme_' + $('#SelectThemeID').val() + '">' + $('#SelectThemeID option:selected').text() + '<span class="glyphicon glyphicon-remove"></div>')
        $('#SelectThemeID').val("");
    });

    $('#parameterContainer').on('click', 'div.parameter', function () {
        $(this).remove();
    });

    $('#btnAdvancedSearch').on('click', function () {
        var cat = new Array;
        var score = new Array;
        $('#parameterContainer').children().each(function (i) {
            cat[i] = $(this).attr('id');
        });

        $('#sliderContainer > div > div > div > p > input').each(function (i) {
            score[i] = $(this).val();
        })

        var SearchParameters =
            {
                'Score': score,
                'Categories': cat
            };

        $.ajax({
            type: 'POST',
            url: 'Advanced/',
            datatype: 'json',
            contentType: 'application/json; charset=utf-8', 
            data: JSON.stringify(SearchParameters),
            success: function (data) { $('.resultContainer').empty().append(data) },
            failure: function (errMsg) { alert(errMsg); }            
        });
    })
});