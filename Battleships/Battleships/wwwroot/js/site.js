function hoverOverWaterCell(cell) {
    var coordinates = $(cell).attr("data-position");
    $(cell).append('<span class="cell-coordinates">' + coordinates + '</span>');
}
function shoot(column, row) {
    $.ajax({
        method: "POST",
        url: "/shoot",
        data: { column: column, row: row }
    })
        .done(function (data) {
            data.cells.forEach(setCellState);

            if (data.isgamewon == true) {
                $('.cell-border').unbind('click');
                $('div.menu').append('<p class="won">You won!</p>');
            }
        });
}

function setCellState(item) {
    var classToSet = '';
    switch (item.state) {
        case 1:
            classToSet = 'waterhit';
            break;
        case 3:
            classToSet = 'shiphit';
            break;
        case 4:
            classToSet = 'shipsunk';
            break;
    }
    var cell = $('[data-position="' + (item.column + item.row) + '"]');
    cell.addClass(classToSet);
}