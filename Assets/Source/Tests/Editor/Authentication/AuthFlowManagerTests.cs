using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using EnvilopeChako.Authentication;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

public class AuthFlowManagerTests
{
    private TestLoginView testLoginView;
    private TestRegisterView testRegisterView;
    private TestVerificationView testVerificationView;

    private LoginController loginController;
    private RegisterController registerController;
    private VerificationController verificationController;

    private AuthFlowManager authFlowManager;

    [SetUp]
    public void Setup()
    {
        // Создаем тестовые представления
        testLoginView = new TestLoginView();
        testRegisterView = new TestRegisterView();
        testVerificationView = new TestVerificationView();

        // Подставляем тестовые данные
        testLoginView.EmailInput = "test@example.com";
        testLoginView.PasswordInput = "password";

        testRegisterView.NicknameInput = "TestUser";
        testRegisterView.EmailInput = "test@example.com";
        testRegisterView.PasswordInput = "password";

        testVerificationView.VerificationCodeInput = "0000";

        // Создаем контроллеры, используя DummyAuthService (служба всегда возвращает true)
        var authService = new DummyAuthService();
        loginController = new LoginController(testLoginView, authService);
        registerController = new RegisterController(testRegisterView, authService);
        verificationController = new VerificationController(testVerificationView, authService);

        // Создаем AuthFlowManager с тестовыми данными
        authFlowManager = new AuthFlowManager(
            testLoginView,
            testRegisterView,
            testVerificationView,
            loginController,
            registerController,
            verificationController
        );
    }

    [TearDown]
    public void TearDown()
    {
        // Очистка ресурсов
        authFlowManager.Dispose();
    }

    [Test]
    public void AuthFlowManager_Initialize_SetsCorrectInitialState()
    {
        // Перед вызовом Initialize установим начальные значения представлений
        testLoginView.SetActive(false);
        testRegisterView.SetActive(true);
        testVerificationView.SetActive(true);

        // Вызываем Initialize в AuthFlowManager
        authFlowManager.Initialize();

        // Проверяем, что только loginView активен
        Assert.IsTrue(testLoginView.IsActive, "Login view should be active after initialization.");
        Assert.IsFalse(testRegisterView.IsActive, "Register view should be inactive after initialization.");
        Assert.IsFalse(testVerificationView.IsActive, "Verification view should be inactive after initialization.");
    }

    [Test]
    public void AuthFlowManager_OnRegistrationInitiated_ActivatesVerificationView()
    {
        // Имитируем начальное состояние
        authFlowManager.Initialize();

        // Имитируем регистрацию: сначала активируем представление регистрации
        testRegisterView.SetActive(true);

        // Вызываем внутренний метод (через reflection) для переключения на экран верификации
        authFlowManager.GetType()
            .GetMethod("ShowVerificationScreen", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(authFlowManager, null);

        // Проверяем, что registerView не активен, а verificationView активен
        Assert.IsFalse(testRegisterView.IsActive, "Register view should be inactive after triggering registration.");
        Assert.IsTrue(testVerificationView.IsActive, "Verification view should be active after triggering registration.");
    }

    [UnityTest]
    public IEnumerator AuthFlowManager_OnLoginSubmit_ActivatesMainScreen()
    {
        // Предположим, что после успешного входа в систему представления закрываются (например, переход на главный экран).
        // Имитируем инициализацию
        authFlowManager.Initialize();
        
        // Допустим, у нас есть метод loginController.HandleLogin, который вызывается при клике на кнопку входа
        loginController.GetType()
            .GetMethod("HandleLogin", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(loginController, null);
        
        yield return UniTask.Delay(1000).ToCoroutine();
        // В данном примере предполагается, что все представления закрываются после успешного входа
        Assert.IsFalse(testLoginView.IsActive, "Login view should be inactive after successful login.");
        Assert.IsFalse(testRegisterView.IsActive, "Register view should be inactive after successful login.");
        Assert.IsFalse(testVerificationView.IsActive, "Verification view should be inactive after successful login.");
    }

    [UnityTest]
    public IEnumerator AuthFlowManager_OnVerificationSubmit_SucceedsAndHidesAllViews()
    {
        authFlowManager.Initialize();
        testLoginView.SimulateRegisterClick();
        yield return UniTask.Delay(1000).ToCoroutine();
        testRegisterView.SimulateRegisterClick();
        yield return UniTask.Delay(1000).ToCoroutine();
        testVerificationView.SimulateVerificationClick();
        yield return UniTask.Delay(1000).ToCoroutine();
        // Assert expected state after asynchronous processing
        Assert.IsFalse(testLoginView.IsActive, "Login view should be inactive after successful verification.");
        Assert.IsFalse(testRegisterView.IsActive, "Register view should be inactive after successful verification.");
        Assert.IsFalse(testVerificationView.IsActive, "Verification view should be inactive after successful verification.");
    }

    [UnityTest]
    public IEnumerator AuthFlowManager_OnRegistrationFailed_ShowsErrorMessageAndKeepsRegisterViewActive()
    {
        // Для теста неудачной регистрации мы создадим фиктивный случай.
        // Например, если email имеет некорректный формат, DummyAuthService (или другая реализация мока) может вернуть ошибку.
        authFlowManager.Initialize();
        testLoginView.SimulateRegisterClick();
        yield return UniTask.Delay(1000).ToCoroutine();
        // Устанавливаем неправильный email, чтобы симулировать ошибку
        testRegisterView.EmailInput = "";

        testRegisterView.SimulateRegisterClick();
        yield return UniTask.Delay(1000).ToCoroutine();

        // Проверяем, что представление регистрации остается активным и выводится сообщение об ошибке
        Assert.IsTrue(testRegisterView.IsActive, "Register view should remain active when registration fails.");
        Assert.IsFalse(testVerificationView.IsActive, "Verification view should not be active if registration fails.");
        Assert.IsNotNull(testRegisterView.Message, "An error message should be displayed.");
        Assert.IsTrue(testRegisterView.Message.Length > 0, "The error message should not be empty.");
    }
}