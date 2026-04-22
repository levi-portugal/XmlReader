<template>
    <div class="quadro">
        <h2>Filtrar XMLs:</h2>

        <input v-model="issuerDocument" placeholder="CNPJ Emitente" />
        <input v-model="recipientDocument" placeholder="CNPJ Destinatário" />
        <input v-model="shipperCnpj" placeholder="CNPJ Transportador" />
        <input v-model="serviceTakerCnpj" placeholder="CNPJ Tomador" />
        <input v-model="recipientName" placeholder="Nome Destinatário" />

        <label style="color: #212121; display: flex;flex-direction: column;">
            Data inicio:
            <input type="date" v-model="startDate" style="color: #212121;" />
        </label>

        <label style="color: #212121; display: flex;flex-direction: column;">
            Data final:
            <input type="date" v-model="endDate" placeholder="Data final" style="color: #212121;" />
        </label>
        <button @click="filtrarXml()">Filtrar</button>
    </div>
    <DataTable :value="listaFiltrada" v-if="listaFiltrada.length > 0" paginator :rows="10"
        :rowsPerPageOptions="[5, 10, 20, 50]" showGridlines>
        <Column field="key" header="Key" />
        <Column field="xmlNumber" header="XML Number" />
        <Column field="issuerDocument" header="Issuer Document" />
        <Column field="emissionDate" header="Emission Date" />
        <Column field="socialReasonIssuer" header="Social Reason Issuer" />
        <Column field="recipientDocument" header="Recipient Document" />
        <Column field="socialReasonRecipient" header="Social Reason Recipient" />
        <Column field="type" header="Type" />
        <Column field="serviceTakerCnpj" header="Service Taker CNPJ" />
        <Column field="shipperCnpj" header="Shipper CNPJ" />
        <Column field="recipientName" header="Recipient Name" />

    </DataTable>


    <!-- <ul>
        <li v-for="(item, index) in listaFiltrada" :key="index">
          {{ item }}
   
        </li>
    </ul> -->

</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

import DataTable from 'primevue/datatable';
import Column from 'primevue/column';


const API_URL = 'https://localhost:7058/api/xml'

const erro = ref(null)
const carregando = ref(false)

const issuerDocument = ref('')
const recipientDocument = ref('')
const shipperCnpj = ref('')
const serviceTakerCnpj = ref('')
const recipientName = ref('')
const startDate = ref('')
const endDate = ref('')
const listaFiltrada = ref([])

async function filtrarXml() {
    try {
        carregando.value = true
        erro.value = null

        const response = await axios.get(API_URL, {
            params: {
                issuerDocument: issuerDocument.value || null,
                recipientDocument: recipientDocument.value || null,
                shipperCnpj: shipperCnpj.value || null,
                serviceTakerCnpj: serviceTakerCnpj.value || null,
                recipientName: recipientName.value || null,
                startDate: startDate.value || null,
                endDate: endDate.value || null
            }
        })

        listaFiltrada.value = response.data
    } catch (err) {
        console.error(err)
        erro.value = 'Erro ao filtrar XMLs'
    } finally {
        carregando.value = false
    }
}
</script>

<style>
.quadro {
    background-color: #ffff;
    box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
    border-radius: 10px;

    h2 {
        color: #212121;
    }
}

.p-datatable .p-datatable-thead>tr>th {
    background: #f4f6fb;
    color: #6c91cc;
    font-size: 0.75rem;
    text-transform: uppercase;
    border: 1px solid #dde3f0 !important;
    text-align: center;
    padding: 12px 15px;

}

.p-datatable-table-container {
    margin-top: 30px;
    border-radius: 8px;
}

/* Linhas zebradas */
.p-datatable .p-datatable-tbody>tr {
    background: #ffffff;
}

.p-datatable .p-datatable-tbody>tr:nth-child(even) {
    background: #f9fbff;
}

/* Células */
.p-datatable .p-datatable-tbody>tr>td {
    padding: 12px 15px;
    border: 1px solid #eef1f8 !important;
    color: #333;
    text-align: center;
}

/* Hover */
.p-datatable .p-datatable-tbody>tr:hover {
    background: #eef3fc !important;
}
</style>