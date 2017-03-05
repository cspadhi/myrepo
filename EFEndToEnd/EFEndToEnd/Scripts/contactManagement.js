/*global $, jQuery*/
if (!window.contactManagement) {
    window.contactManagement = (function () {
        var baseUrl = '';

        var handleJqGridLoadError = function(xhr, st, str) {
            alert('Error');
        };
        
        var setupContactsTypesGrid = function () {

            var deleteImageFormat = function(cellVal) {
                return '<a href="' + baseUrl + 'contactType/delete/' + cellVal + '">' + '<img src="' + baseUrl + 'Content/Images/Delete.png" title = "Delete" /></a>';
            };
            
            var editImageFormat = function (cellVal) {
                return '<a href="' + baseUrl + 'contactType/edit/' + cellVal + '">' + '<img src="' + baseUrl + 'Content/Images/Edit.png" title = "Delete" /></a>';
            };

            $('#typesGrid').jqGrid({

                colNames: ['', 'Name', 'Edit', 'Delete'],
                colModel: [
                { name: 'Id', index: 'Id', hidden: true },
                { name: 'Name', width: 500, index: 'Name', align: 'left', searchoptions: { sopt: ['cn'] } },
                { name: 'Edit', width: 50, index: 'Edit', sortable: false, search: false, align: 'center', formatter: editImageFormat },
                { name: 'Delete', width: 50, index: 'Delete', sortable: false, search: false, align: 'center', formatter: deleteImageFormat }
                ],
                sortname: 'Name',
                sortorder: "asc",
                scroll: false,
                rowNum: 10,
                rowList: [1,10,50],
                datatype: 'json',
                caption: '',
                viewrecords: true,
                mtype: 'GET',
                height: '100%',
                width: '100%',
                pager: $('#typesPager'),
                url: baseUrl + 'ContactType/GetTypes',
                loadError: handleJqGridLoadError
            }).navGrid('#typesPager', { view: false, del: false, add: false, edit: false },
                  {}, // default settings for edit
                  {}, // default settings for add
                  {}, // delete
                  { closeOnEscape: true, multipleSearch: false, closeAfterSearch: true }, // search options
                  { dataheight: '100%' });
        };
        var init = function() {
            setupContactsTypesGrid();
            $('#createTypeButton').button();
            $('#createTypeButton').click(function() {
                window.location = baseUrl + 'ContactType/Create';
            });
        };
        
        var setupContactsGrid = function () {

            var deleteImageFormat = function (cellVal) {
                return '<a href="' + baseUrl + 'contact/delete/' + cellVal + '">' + '<img src="' + baseUrl + 'Content/Images/Delete.png" title = "Delete" /></a>';
            };

            var editImageFormat = function (cellVal) {
                return '<a href="' + baseUrl + 'contact/edit/' + cellVal + '">' + '<img src="' + baseUrl + 'Content/Images/Edit.png" title = "Delete" /></a>';
            };

            $('#contactsGrid').jqGrid({

                colNames: ['', 'Last Name', 'First Name', 'Edit', 'Delete'],
                colModel: [
                { name: 'Id', index: 'Id', hidden: true },
                { name: 'LastName', width: 250, index: 'LastName', align: 'left', searchoptions: { sopt: ['cn'] } },
                { name: 'FirstName', width: 250, index: 'FirstName', align: 'left', searchoptions: { sopt: ['cn'] } },
                { name: 'Edit', width: 50, index: 'Edit', sortable: false, search: false, align: 'center', formatter: editImageFormat },
                { name: 'Delete', width: 50, index: 'Delete', sortable: false, search: false, align: 'center', formatter: deleteImageFormat }
                ],
                sortname: 'LastName',
                sortorder: "asc",
                scroll: false,
                rowNum: 10,
                rowList: [1, 10, 50],
                datatype: 'json',
                caption: '',
                viewrecords: true,
                mtype: 'GET',
                height: '100%',
                width: '100%',
                pager: $('#contactsPager'),
                url: baseUrl + 'Contact/GetContacts',
                loadError: handleJqGridLoadError
            }).navGrid('#contactsPager', { view: false, del: false, add: false, edit: false },
                  {}, // default settings for edit
                  {}, // default settings for add
                  {}, // delete
                  { closeOnEscape: true, multipleSearch: false, closeAfterSearch: true }, // search options
                  { dataheight: '100%' });
        };

        var initContacts = function () {
            setupContactsGrid();
            $('#createContactButton').button();
            $('#createContactButton').click(function () {
                window.location = baseUrl + 'Contact/Create';
            });
        };


        return {
            init: init,
            initContacts: initContacts,
            setUrl: function(url) {
                baseUrl = url;
            }
        };
    }());
   
}