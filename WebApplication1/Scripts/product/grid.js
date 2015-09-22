var Product = function () {
    var grid = $('#grid');
    var pager = '#pager';
    var produModal = $('#product_modal');
    var deleteModal = $('#delete_modal');
    var form = $('#product_form');
    var deleteForm = $('#delete_form');

    var productTypeDropDown = $('#ProductTypeDropDown');
    var supplierDropDown = $('#SupplierDropDown')
    //var name = $('#Name');
    //var id = $('#Id');

    this.getUrl = '';
    this.createUrl = '';
    this.editUrl = '';
    this.deleteUrl = '';

    var rowData = null;

    var me = this;

    var createGrid = function () {
        $.jgrid.defaults.responsive = true;
        $.jgrid.defaults.styleUI = 'Bootstrap';

        //var myData = [{ Id: 1, Name: 'Product1' }, { Id: 2, Name: 'Product2' }];
        grid.jqGrid({
            url: me.getUrl,
            mtype: 'GET',
            editurl: 'clientArray',
            datatype: 'json',
            //data: myData,
            colNames: ['Id', 'ชื่อสินค้า', 'ประเภทสินค้า', 'ProductTypeId', 'บริษัท', 'SupplierId'],
            colModel: [
                { name: 'Id', index: 'Id', key: true, hidden: true },
                { name: 'Name', index: 'Name', search: true, width: 180, fixed: true, align: 'right' },
                { name: 'ProductTypeName', index: 'ProductTypeName', search: true, width: 100, fixed: true, align: 'left' },
                { name: 'ProductTypeId', index: 'ProductTypeId', hidden: true },
                { name: 'SupplierName', index: 'SupplierName', search: true, width: 100, fixed: true, align: 'left' },
                { name: 'SupplierId', index: 'SupplierId', hidden: true }
            ],
            rowNum: 10,
            rowList: [10, 20, 30],

            onSelectRow: function (id) {
                rowData = grid.jqGrid('getRowData', id);
                console.log(rowData);
            },

            postData: { productTypeId: function () { return productTypeDropDown.val(); }, supplierId: function () { return supplierDropDown.val(); } },

            pager: pager,
            autowidth: true,
            height: 400,
            caption:'รายการสินค้า'

        });

        grid.jqGrid('filterToolbar', { beforeSearch: true })
        grid.jqGrid('navGrid', pager, {
            edit: true, add: true, del: true, refresh: true, search: false,
            addfunc: function () {
                console.log('create Url :' + me.createUrl);
                form.attr('action', me.createUrl);
                form.clearForm();
                produModal.modal('show');
            },
            editfunc: function () {
                console.log('create Url :' + me.editUrl);
                form.attr('action', me.editUrl);
                form.clearForm();
                FormHelper.setFormValue(rowData, form);

                produModal.modal('show');
            },
            delfunc: function () {

                //console.log('create Url :' + me.deleteUrl);
                deleteForm.attr('action', me.deleteUrl);
                FormHelper.setFormValue(rowData, deleteForm);
                deleteModal.modal('show');

            }


        });
    };

    this.init = function () {
        createGrid();

        form.ajaxForm({
            dataType: 'json',
            success: processJson
        });

        deleteForm.ajaxForm({
            dataType: 'json',
            success: processJson
        });

        productTypeDropDown.change(function () {
            grid.trigger('reloadGrid');
        });

        supplierDropDown.change(function () {
            grid.trigger('reloadGrid');
        });
    };

    var processJson = function (data) {
        produModal.modal('hide');
        deleteModal.modal('hide');
        grid.trigger('reloadGrid');
    }


};