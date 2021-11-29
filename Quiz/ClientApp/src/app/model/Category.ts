interface Category {
  id: number;
  name: string;
  subCategories: Category[];
  parentCategoryId?: number;
}

interface Question {
  id: number;
  categoryId: number;
  content: string;
  answer: string;
}

interface Quiz {
  id: number;
}

interface UpdatedQuestionWithAnswer {
  QuestionContent: string,
  AnswerContent: string
}
interface AnswerResult {
  result: boolean;
}

interface Answer {
  id: number,
  content: string;
}

interface CreateSubcategory {
  parentId: number,
  name: string
}

interface QuestionDto {
  id: number;
  categoryId: number;
  content: string;
}

interface QuestionWithAnswer {
  question: QuestionDto,
  answer: Answer
  status?: StatusCode
}

interface QuizStatusId {
  status: QuizStatus,
  id:number

}

interface SignUpData{
  email: string;
  password: string;
  firstName: string;
  lastName: string;
}

interface SignInData {
  email: string;
  password: string;
}

interface User {
  email: string;
  id: number;
  role: string;
  name: string;
  token: string;
}

interface QuestionWithStatus {
  questionDto: QuestionDto,
  quizStatusDto: QuizStatusDto;
}

interface QuizStatusDto {
  id: number;
  status: number;
}

interface QuizError {
  name: string;
  message: string;
}

enum QuizStatus {
  NotStarted, Active, Finished
}
enum StatusCode {
  Unchanged,
  Added,
  Deleted,
  Updated
}
