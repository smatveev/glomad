
<template v-if="">

  <div class="hero-bg px-4 py-5 text-white text-center">
    <h1 class="display-5 fw-bold">{{ embassy.country }}</h1>

    <div class="col-lg-6 mx-auto position-absolute">
        <p class="lead mb-4">
        <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="/">Home</a></li>
        <li class="breadcrumb-item"><a href="/Thailand">Thailand</a></li>
        <li class="breadcrumb-item" aria-current="page">
          Embassy of Thailand in {{ embassy.country }}
        </li>
      </ol>
    </nav>
      </p>
      
    </div>
  </div>

  <section class="container py-3">    

    <Tabs>
      <Tab title="Info">
        <h2 class="fw-bold text-center p-4 mb-3">🏢 Embassy details in {{ embassy.country }}</h2>
        <p>📍 <small class="text-muted">Address: </small>{{ embassy.embassy.address }}</p>
        <p>🕜 <small class="text-muted">Office hours: </small>{{ embassy.embassy.workingHours }}</p>
        <p>☎ <small class="text-muted">Phone: </small>{{ embassy.embassy.phone }}</p>
        <p>📧 <small class="text-muted">Email: </small>{{ embassy.embassy.email }}</p>
        <p>💻 <small class="text-muted">Website: </small><a :href="'//' + embassy.embassy.url" target="_blank">{{ embassy.embassy.url }}</a></p>
        <p>🚴‍♂️ <small class="text-muted">Processing time: </small>{{ embassy.embassy.processingTime }}</p>
        <p>☝ <small class="text-muted">Special conditions: </small>{{ embassy.embassy.specialConditions }}</p>
        <p>🐱‍💻 <small class="text-muted">Visa application form: </small><a :href="embassy.embassy.applicationFormUrl" target="_blank"> link</a></p>
      </Tab>

      <Tab title="Documents">
        <h2 class="fw-bold text-center p-4">Checklist of documents for visa to Thailand</h2>

        <ul v-for="d in docs" :key="d.id">
          <li>{{ d.text }}</li>
        </ul>
      </Tab>

      <Tab title="Visas">
        <h2 class="fw-bold text-center p-4">Types of visas allowed at the embassy</h2>
        <table class="table table-hover">
          <thead>
            <tr>
              <th scope="col">Name</th>
              <th scope="col">Description</th>
              <th scope="col">Duration</th>
              <th scope="col">Extendable</th>
              <th scope="col">Price</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="v in visas" :key="v.name">
              <th scope="row">{{ v.name }}</th>
              <td>{{ v.description }}</td>
              <td>{{ v.duration }}</td>
              <td>{{ v.isExtendable ? "Yes" : "No" }}</td>
              <td>{{ v.currency }} {{ v.price }}</td>
            </tr>
          </tbody>
        </table>
      </Tab>
    </Tabs>
  </section>
</template>

<script> 
import Tabs from "@/components/Tabs.vue";
import Tab from "@/components/Tab.vue";

export default {
  //props: ['id'],
  data() {
    return {
      id: this.$route.params.id,
      embassy: {
        embassy: {}
      },
      docs: {},
      visas: {}
    };
  },
  mounted() {
    fetch(process.env.VUE_APP_API_URL + "Embassies?id=" + this.id)
      .then((res) => res.json())
      .then((data) => {
        this.embassy = data;
        console.info(data);
      })
      .catch((err) => {
        console.log(err.message);
      } );

    fetch(process.env.VUE_APP_API_URL + "Embassies/DocsById?id=" + this.id)
      .then((res) => res.json())
      .then((data) => {
        this.docs = data;
        console.info(data);
      })
      .catch((err) => {
        console.warn(err.message);
      });

    fetch(process.env.VUE_APP_API_URL + "Embassies/VisasById?id=" + this.id)
      .then((res) => res.json())
      .then((data) => {
        this.visas = data;
        console.info(data);
      })
      .catch((err) => {
        console.warn(err.message);
      });      
  },
  components: {
    Tabs,
    Tab,
  },
};
</script>
