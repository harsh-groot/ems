import axios from "axios";

export const putApiService = async (url,body, config) => {
  return  await axios.put(url, body, config)
        .then((res) => {
            return res
        })
      .catch((err) => {
            return err
        })
}