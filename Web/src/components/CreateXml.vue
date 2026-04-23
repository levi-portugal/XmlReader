<template>
  <div class="quadro">
    <h2>Criar XML (base64):</h2>

    <input v-model="xmlBase64" placeholder="Cole o base64" />

    <button @click="criarXml">Criar</button>


    <FileUpload

      mode="basic"
      accept=".xml"
      :auto="false"
      customUpload
      @select="onFileSelect"
      chooseLabel="Selecionar XML"
    />

    <button @click="uploadXml">Enviar</button>

    <p v-if="respostaCriacao">{{ respostaCriacao }}</p>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'
import FileUpload from 'primevue/fileupload'

const API_URL = 'https://localhost:7058/api/xml'
const API_URL2 = 'https://localhost:7058/api/xml/upload'

const xmlBase64 = ref('')
const respostaCriacao = ref('')
const file = ref(null)

const erro = ref(null)
const carregando = ref(false)

async function criarXml() {
  try {
    carregando.value = true
    erro.value = null

    const response = await axios.post(
      API_URL,
      xmlBase64.value,
      {
        headers: {
          'Content-Type': 'application/json'
        }
      }
    )

    respostaCriacao.value = response.data.mensagem
  } catch (err) {
    console.error(err)
    erro.value = 'Erro ao criar XML'
  } finally {
    carregando.value = false
  }
}

// pega o arquivo do prime
function onFileSelect(event) {
  file.value = event.files[0]
  console.log("Arquivo salvo:", file.value)
}

// envia o arquivo do prime
async function uploadXml() {
  if (!file.value) {
    console.log("Nenhum arquivo selecionado")
    return
  }

  try {
    carregando.value = true
    erro.value = null

    const formData = new FormData()
    formData.append("archive", file.value)

    const response = await axios.post(API_URL2, formData)

    respostaCriacao.value = response.data.mensagem
  } catch (err) {
    console.error(err)
    erro.value = 'Erro ao enviar XML'
  } finally {
    carregando.value = false
  }
}

</script>

<style >
   
 .p-fileupload-basic-content {
    display: flex;
    flex-wrap: wrap;
    align-items: center;
    color: #000;
    gap: var(--p-fileupload-basic-gap);
}
    .quadro input{
        height: 45px;
        border: 1px solid #d1d5db;
        border-radius: 6px;
        padding: 0 15px;
        transition: border-color 0.2s;
  
    }
    

    .quadro{
        background-color: #ffff;
        box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
        border-radius: 10px;
        h2{
            color: #212121;
        }
    }

    .quadro .button{
      margin-right: 20px;
    }
</style>