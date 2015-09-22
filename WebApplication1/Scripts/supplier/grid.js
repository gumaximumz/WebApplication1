Supplier.Grid = function () {
    this.grid = $('#grid_supplier');
    this.pager = '#paper_supplier';
    this.supplierModal = $('#supplier_modal');
    this.deletesupplierModal = $('#delete_supplier_modal')
    this.getUrl = '';
    this.createUrl = '';
    this.editUrl = '';
    this.deleteUrl = '';
    this.form = $('#supplier_form');
    this.deleteFrom = $('#delete_supplier_form')
    this.rowData = null;
};

Supplier.Grid.prototype.init = function () {
    this.createGird();
    var me = this;
    var processJson = function (data) {
        me.supplierModal.modal('hide');
        me.deletesupplierModal.modal('hide');
        me.grid.trigger('reloadGrid');
    };

    this.form.ajaxForm({
        dataType: 'json',
        success: processJson
    });

    this.deleteFrom.ajaxForm({
        dataType: 'json',
        success: processJson
    });
};

Supplier.Grid.prototype.createGird = function () {
    var me = this;
    $.jgrid.defaults.responseive = true;
    $.jgrid.defaults.styleUI = 'Bootstrap';
    this.grid.jqGrid({
        url: me.getUrl,
        mtype: "GET",
        editurl: 'clientArray',
        datatype: 'json',
        colNames: ['Id', 'บริษัท'],
        colModel: [
            { name: 'Id', index: 'Id', key: true, hidden: true },
            { name: 'Name', index: 'Name', search: true, width: 180, fixed: true, align: 'right' }
        ],
        rowNum: 10,
        rowList: [10, 20, 30],

        onSelectRow: function (id) {
            me.rowData = me.grid.jqGrid('getRowData', id);
            console.log(me.rowData)
        },

        pager: me.pager,
        autowidth: true,
        height: 400,
        caption: 'รายการชื่อบริษัท'
    });

    this.grid.jqGrid('filterToolbar', { beforeSearch: true })
    this.grid.jqGrid('navGrid', this.pager, {
        edit: true, add: true, del: true, refresh: true, search: false,
        addfunc: function () {
            console.log('create Url :' + me.createUrl);
            me.form.attr('action', me.createUrl);
            me.form.clearForm();
            me.supplierModal.modal('show');
        },
        editfunc: function () {
            console.log('create Url :' + me.editUrl);
            me.form.attr('action', me.editUrl);
            me.form.clearForm();
            FormHelper.setFormValue(me.rowData, me.form);
            me.supplierModal.modal('show')
        },
        delfunc: function () {
            console.log('create Url :' + me.deleteUrl);
            me.deleteFrom.attr('action', me.deleteUrl);
            FormHelper.setFormValue(me.rowData, me.deletesupplierModal);
            me.deletesupplierModal.modal('show');
        }
    });
};