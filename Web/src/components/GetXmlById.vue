<template>
    <div class="bloco">
      <h2>Buscar XML pela chave:</h2>

      <input v-model="id" placeholder="Digite o ID" />

      <button @click="pegarPorId">Buscar</button>

      <button v-if="resultadoId" @click="download(resultadoId)">Download</button>


      <p v-if="resultadoId">{{ resultadoId }}</p>
    </div>

</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const id = ref('')
const resultadoId = ref('')

const erro = ref(null)
const carregando = ref(false)

const API_URL = 'https://localhost:7058/api/xml'

async function pegarPorId (){
  try {
    carregando.value = true
    erro.value = null

    const response = await axios.get(`${API_URL}/${id.value}`)
    
    resultadoId.value = response.data
  } catch (err) {
    console.log(err)
    erro.value = 'Erro ao buscar o Xml'
  } finally {
    carregando.value = false
  }
}

async function download (conteudoRef){

const conteudo = conteudoRef.value ?? conteudoRef

try {
    const blob = new Blob([conteudo], { type: 'application/xml;charset=utf-8;' })

    const url = window.URL.createObjectURL(blob)

    const link = document.createElement('a')
    link.href = url
    link.setAttribute('download', 'nf.xml')

    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
} catch (err) {
    console.error("Erro ao baixar um arquivo", err)
}

    
}
</script>

<style scoped>
    .bloco{
        p{
            font-family: monospace; 
            border-radius: 10px;
            width: 100vh;
            max-height: 300vh;
            overflow-wrap: break-word;
            color: #212121;
          
        }
        background-color: #FFFFFF;
        box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
        border-radius: 10px;
          h2{
            color: #212121;
        }
    }
</style>
