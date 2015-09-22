ProductType.Grid = function () {
    this.grid = $('#grid_productType');
    this.pager = '#pager_productType';
    this.producttypeModal = $('#producttype_modal');
    this.deletetypeModal = $('#deletetype_modal');
    this.getUrl = '';
    this.createUrl = '';
    this.editUrl = '';
    this.deleteUrl = '';
    this.form = $('#producttype_form');
    this.deletetypeForm = $('#deletetype_form');
    this.rowData = null;
};

ProductType.Grid.prototype.init = function () {
    this.createGrid();
    var me = this;
    var processJson = function (data) {
        console.log('create Url :' + me.deleteUrl)
        me.producttypeModal.modal('hide');
        me.deletetypeModal.modal('hide');
        me.grid.trigger('reloadGrid');
    };

    this.form.ajaxForm({
        dataType: 'json',
        success: processJson
    });

    this.deletetypeForm.ajaxForm({
        dataType: 'json',
        success: processJson
    });
};

ProductType.Grid.prototype.createGrid = function () {
    var me = this;
    $.jgrid.defaults.responsive = true;
    $.jgrid.defaults.styleUI = 'Bootstrap';
    this.grid.jqGrid({
        url: me.getUrl,
        mtype: 'GET',
        editurl: 'clientArray',
        datatype: 'json',
        colNames: ['Id', 'ประเภทสินค้า'],
        colModel: [
            { name: 'Id', index: 'Id', key: true, hidden: true },
            { name: 'Name', index: 'Name', search: true, width: 180, fixed: true, align: 'right' },
        ],
        rowNum: 10,
        rowList: [10, 20, 30],

        onSelectRow: function (id) {
            me.rowData = me.grid.jqGrid('getRowData', id);
            console.log(me.rowData);
        },

        pager: me.pager,
        autowidth: true,
        height: 400,
        caption: 'รายการประเภทสินค้า'
    });

    this.grid.jqGrid('filterToolbar', { beforeSearch: true })
    this.grid.jqGrid('navGrid', this.pager, {
        edit: true, add: true, del: true, refresh: true, search: false,
        addfunc: function () {
            console.log('create Url :' + me.createUrl);
            me.form.attr('action', me.createUrl);
            me.form.clearForm();
            me.producttypeModal.modal('show');
        },
        editfunc: function () {
            console.log('create Url :' + me.editUrl);
            me.form.attr('action', me.editUrl);
            me.form.clearForm();
            FormHelper.setFormValue(me.rowData, me.form);
            me.producttypeModal.modal('show');
        },
        delfunc: function () {
            console.log('create Url :' + me.deleteUrl);
            me.deletetypeForm.attr('action', me.deleteUrl);
            FormHelper.setFormValue(me.rowData, me.deletetypeForm);
            me.deletetypeModal.modal('show');
        }
    });   
};