<template>

    <Dialog v-model:show="priceVisible">
      <div class="container">
        <div class="py-5 m-0 row">
          <div class="col-sm-12 col-lg-8">
          <h3 class="pb-3">Do you think about where to go next? We are glad to help you.</h3>
              <p>😷 COVID-19 travel restriction in selected countries.</p>
              <p>🎫 The range of visa opportunities for digital nomads.</p>
              <p>🛂 The longest duration of stay according to visa rules. </p>
              <p>😎 The easiest way to apply for a visa.</p>
              <p>✅ Checklist of documents to apply for a visa.</p>
              <p>⌛ Is your visa extendable? Extension conditions.</p>
              <p>✈ Avia tickets costs. </p>
              <p>🌅 Climatic conditions.</p>
              <h5>For 1 country or for 5. Special offer for your situation. Everything depends on the selected plan.
                Forget about visa headaches. Be ready to travel at any time.</h5>
          </div>
            <div class="col-sm-12 col-lg-4">
              <form>
                <div class="form-floating mb-3">
                  <input
                    type="email"
                    class="form-control"
                    placeholder="name@example.com"
                  />
                  <label>Email address</label>
                </div>
                <div class="form-floating mb-3">
                  <input
                    type="text"
                    class="form-control"
                    placeholder="Your name"
                  />
                  <label>Your name</label>
                </div>

        <button class="w-100 btn btn-lg btn-primary" type="submit">
          Thank you ✌
        </button>

      </form>
            </div>

            <p>From momads for nomads with ❤.</p>
        </div>
      </div>
    </Dialog>

    <div class="row row-cols-1 row-cols-md-3 mb-3 text-center">
      
      <div class="col">
        <div class="card mb-4 rounded-3 shadow-sm">
          <div class="card-header py-3">
            <h4 class="my-0 fw-normal">One country</h4>
          </div>
          <div class="card-body">
            <h1 class="card-title pricing-card-title">$5<small class="text-muted fw-light">/one time</small></h1>
            <ul class="list-unstyled mt-3 mb-4">
              <li>🛂 Visa details</li>
              <li>🎫 Document list</li>
              <li>😷 Covid restrictions</li>
            </ul>
            <button type="button" class="w-100 btn btn-lg btn-primary">Get started</button>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card mb-4 rounded-3 shadow-sm">
          <div class="card-header py-3">
            <h4 class="my-0 fw-normal">5 countries</h4>
          </div>
          <div class="card-body">
            <h1 class="card-title pricing-card-title">$20<small class="text-muted fw-light">/one time</small></h1>
            <ul class="list-unstyled mt-3 mb-4">
              <li>🛂 Visa details</li>
              <li>🎫 Document list</li>
              <li>😷 Covid restrictions</li>
            </ul>
            <button type="button" class="w-100 btn btn-lg btn-primary">Get started</button>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card mb-4 rounded-3 shadow-sm border-primary">
          <div class="card-header py-3 text-white bg-primary border-primary">
            <h4 class="my-0 fw-normal">Personal</h4>
          </div>
          <div class="card-body">
            <h1 class="card-title pricing-card-title">$25<small class="text-muted fw-light">/one time</small></h1>
            <ul class="list-unstyled mt-3 mb-4">
              <li>Individualy prepare 3 destinaion points</li>
              <li>based on your current situation</li>
            </ul>
            <button type="button" class="w-100 btn btn-lg btn-primary" @click="showDialog()">Get started</button>
          </div>
        </div>
      </div>
    </div>
</template>

<script>

import Dialog from '@/components/UI/Dialog.vue'

export default{
  data() {
    return {
        priceVisible: false,
    }
  },
  methods: {
    showDialog() {
        this.priceVisible = true;
    }
  },
  components: {
    Dialog
  }
}
</script>
