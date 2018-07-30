(function () {
    'use strict';

    angular.module('app').factory('produtoFactory', produtoFactory); 

    produtoFactory.$inject = ['$http'];

    function produtoFactory($http) {
        var service = {
            getData: getData,
            getProdutos: getProdutos,
            insert: insert,
            update: update,
            deleteObj: deleteObj
        };

        return service;

        function getData() {
            return $http.get('/api/produto/GetAll');
        }

        function getProdutos(descricao) {
            var config = {
                params: {
                    descricao: descricao
                }
            }
            return $http.get('/api/produto/GetByDescricao', config);
        }

        function insert(obj) {
            return $http.post('/api/produto/Insert', obj);
        }

        function update(obj) {
            return $http.put('/api/produto/Update', obj);
        }

        function deleteObj(id) {
            return $http.delete('/api/produto/Delete/' + id);
        }

    }
})();