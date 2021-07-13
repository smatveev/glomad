
<template v-if="embassy">
  <section class="container py-3">
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="/">Home</a></li>
        <li class="breadcrumb-item"><a href="/Thailand">Thailand</a></li>
        <li class="breadcrumb-item active" aria-current="page">
          Embassy of Thailand at {{ embassy.country }}
        </li>
      </ol>
    </nav>

    <Tabs>
      <Tab title="Info">
        <h2 class="fw-bold text-center p-4 mb-3">🏢 Embassy details in {{ embassy.country }}</h2>
        <p>📍 <small class="text-muted">Address: </small>{{ embassy.embassy.address }}</p>
        <p>🕜 <small class="text-muted">Office hours: </small>{{ embassy.embassy.workingHours }}</p>
        <p>☎ <small class="text-muted">Phone: </small>{{ embassy.embassy.phone }}</p>
        <p>📧 <small class="text-muted">Email: </small>{{ embassy.embassy.email }}</p>
        <p>💻 <small class="text-muted">Website: </small><a :href="'//' + embassy.embassy.url" target="_blank">{{ embassy.embassy.url }}</a></p>
        <p>🐱‍💻 <small class="text-muted">Web application: </small><a :href="'//' + embassy.embassy.applicationFormUrl" target="_blank"> link</a></p>
      </Tab>

      <Tab title="Documents">
        <h2 class="fw-bold text-center p-4">Checklist of documents for visa to Thailand</h2>

        <ul v-for="d in docs" :key="d.id">
          <li>{{ d.text }}</li>
        </ul>
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
      name: this.$route.params.name,
      embassy: {
        embassy: {}
      },
      docs: {},
    };
  },
  mounted() {
    fetch(process.env.VUE_APP_API_URL + "Embassies?id=" + this.name)
      .then((res) => res.json())
      .then((data) => {
        this.embassy = data;
        console.info(data);
      })
      .catch((err) => {
        console.log(err.message);
      } );

    fetch(process.env.VUE_APP_API_URL + "Embassies/DocsById?id=" + this.name)
      .then((res) => res.json())
      .then((data) => {
        this.docs = data;
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
