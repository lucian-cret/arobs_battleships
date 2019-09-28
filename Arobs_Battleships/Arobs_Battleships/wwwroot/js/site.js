function shoot(column, row) {
    $.ajax({
        method: "POST",
        url: "shoot",
        data: { column: column, row: row }
    })
        .done(function (state) {
            var cell = $('[data-position="' + (column + row) + '"]').attr('data-state', state.value);
            var classToSet = '';
            switch (state.value) {
                case 'IsShipHit':
                    classToSet = 'shiphit';
                    break;
                case 'IsWaterHit':
                    classToSet = 'waterhit';
                    break;
            }
            cell.addClass(classToSet);
        });
}