import axios from "axios";

export const postApiService = async (url,body, config) => {
  return  await axios.post(url, body, config)
        .then((res) => {
            return res
        })
      .catch((err) => {
            return err
        })
}