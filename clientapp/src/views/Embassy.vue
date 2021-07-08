
<template>



<section class="container py-3">

<nav aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item"><a href="/">Home</a></li>
          <li class="breadcrumb-item"><a href="/Thailand">Thailand</a></li>
          <li class="breadcrumb-item active" aria-current="page">Embassy at {{ embassy.countryIata }}</li>
        </ol>
      </nav>

  <Tabs>

<Tab title="Info">
      <h2 class="fw-bold text-center p-4">🏢 Embassy details</h2>

      <p>{{ embassy.countryIata }}</p>
      <p>📍 {{ embassy.address }}</p>
      <p>🕜 {{ embassy.workingHours }}</p>
      <p>☎ {{ embassy.phone }}</p>
      <p>📧 {{ embassy.email }}</p>
      <p> {{ embassy.url }}</p>

    </Tab>

    <Tab title="Documents">
      <h2 class="fw-bold text-center p-4">List of documents</h2>

      <div>{{ docs.text }}</div>
    </Tab>

    
  </Tabs>  
  
</section>  
 
</template>

<script>

import Tabs from '@/components/Tabs.vue'
import Tab from '@/components/Tab.vue'

export default {
  //props: ['id'],
  data() {
    return {      
      name: this.$route.params.name,
      embassy: {},
      docs: {}
    };
  },
  mounted() {
    fetch(process.env.VUE_APP_API_URL + "Embassies?id=" + this.name)
      .then((res) => res.json())
      .then((data) => {
        this.embassy = data;
        console.info(data);
      })
      .catch((err) => console.log(err.message));

    fetch(process.env.VUE_APP_API_URL + "Embassies/DocsById?id=" + this.name)
      .then((res) => res.json())
      .then((data) => {
        this.docs = data;
        console.info(data);
      })
      .catch((err) => {
        console.warn(err.message);
        this.embassy = null;
      });
  },
  components: {
    Tabs,
    Tab
  }
};
</script>
