(function () {
    'use strict';

    angular.module('app').factory('pedidoFactory', pedidoFactory);

    pedidoFactory.$inject = ['$http']; 

    function pedidoFactory($http) {
        var service = {
            getData: getData,
            getMeusPedidos: getMeusPedidos,
            getPedidoProduto: getPedidoProduto,
            insert: insert,
            insertPedidoProduto: insertPedidoProduto,
            deleteObj: deleteObj,
            deletePedidoProduto: deletePedidoProduto,
            finalizarPedido: finalizarPedido
        };

        return service;

        function getData() {
            return $http.get('/api/pedido/GetAll');
        }

        function getMeusPedidos(idCliente) {
            var config = {
                params: {
                    idCliente: idCliente
                }
            }
            return $http.get('/api/pedido/GetMeusPedidos', config);
        }

        function getPedidoProduto(idCliente) {
            var config = {
                params: {
                    idCliente: idCliente
                }
            }
            return $http.get('/api/pedido/GetPedidoProduto', config);
        }

        function insert(obj) {
            return $http.post('/api/pedido/Insert', obj);
        }

        function insertPedidoProduto(obj) {
            return $http.post('/api/pedido/InsertPedidoProduto', obj);
        }

        function deleteObj(id) {
            return $http.delete('/api/pedido/Delete/' + id);
        }

        function deletePedidoProduto(id) {
            return $http.delete('/api/pedido/DeletePedidoProduto/' + id);
        }

        function finalizarPedido(obj) {
            return $http.put('/api/pedido/FinalizarPedido', obj);
        }

    }
})();